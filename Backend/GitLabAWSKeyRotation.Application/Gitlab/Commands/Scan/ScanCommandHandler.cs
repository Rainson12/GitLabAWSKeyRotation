using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient;
using Microsoft.Extensions.Configuration;
using GitLabAWSKeyRotation.Domain.GitLab;
using Amazon.SecurityToken.Model;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{
    public class ScanCommandHandler : IRequestHandler<ScanCommand, ErrorOr<bool>>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IGitlabAccessTokenRepository _accessTokenRepository;
        private readonly IConfiguration _config;

        public ScanCommandHandler(IGitlabAccessTokenRepository accessTokenRepository, IAccountsRepository accountsRepository, IConfiguration config)
        {
            _accessTokenRepository = accessTokenRepository;
            _accountsRepository = accountsRepository;
            _config = config;
        }

        public async Task<ErrorOr<bool>> Handle(ScanCommand command, CancellationToken cancellationToken)
        {
            var gitlabUrl = _config.GetSection("Gitlab").GetValue<string>("Url");

            if (_accessTokenRepository.GetByTokenOrName(command.token, command.name) is not AccessToken accessToken)
            {
                accessToken = Domain.GitLab.AccessToken.Create(command.name, command.token);
                _accessTokenRepository.Add(accessToken);
            }

            Uri gitlabRepoWebUrl = new Uri(gitlabUrl);
            var gitlabRootUrl = gitlabRepoWebUrl.GetLeftPart(UriPartial.Authority);
            var gitlabClient = new GitLabClient(gitlabRootUrl, command.token);
            var allProjects = await gitlabClient.Projects.GetAsync();

            var queryVariablesOfProjectsAsync = allProjects.Select(async x =>
            {
                try
                {
                    var variables = await gitlabClient.Projects.GetVariablesAsync(x.Id);
                    return new { Project = x, Variables = variables };
                }
                catch
                {
                    return null;
                }
                
            }).ToList();
            var queryVariablesOfProjectsResult = await Task.WhenAll(queryVariablesOfProjectsAsync);
            var projectsWithVariables = queryVariablesOfProjectsResult.Where(x => x != null).ToList();
            var projectsWithAWSVariables = projectsWithVariables.Where(x => x.Variables.Any(x => x.Value.Length == 40)).ToList();
            foreach (var matchedProject in projectsWithAWSVariables)
            {
                var environments = matchedProject!.Variables.Where(x => x.Value.Length == 20 || x.Value.Length == 40).GroupBy(x => x.EnvironmentScope).ToList();
                foreach(var env in environments)
                {
                    var envName = env.Key ?? "Default";
                    var accessKeyId = env.FirstOrDefault(x => x.Value.Length == 20);
                    var accessSecret = env.FirstOrDefault(x => x.Value.Length == 40);
                    
                    if (accessKeyId != null && accessSecret != null)
                    {
                        try
                        {
                            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKeyId.Value, accessSecret.Value);
                            var stsClient = new Amazon.SecurityToken.AmazonSecurityTokenServiceClient(awsCredentials);
                            var callerIdRequest = new GetCallerIdentityRequest();
                            var result = await stsClient.GetCallerIdentityAsync(callerIdRequest); // validate iam credentials are correct
                            var iamArn = result.Arn;
                            var accountId = result.Account;
                            var iamUserName = iamArn.Split("/").Last();

                            if(accessToken.CodeRepositories.FirstOrDefault(x => x.Url == matchedProject.Project.WebUrl) is not CodeRepository codeRepo) // get repo, otherwise register it
                            {
                                codeRepo = CodeRepository.Create(matchedProject.Project.WebUrl, matchedProject.Project.Name);
                                accessToken.AddCodeRepository(codeRepo);
                            }
                            if(_accountsRepository.Get(accountId) is not Account account) // get aws account, otherwise register it
                            {
                                account = Account.Create(accountId, accountId);
                                _accountsRepository.Add(account);
                            }
                            if(account.IamIdentities.FirstOrDefault(x => x.AccessKeyId ==  accessKeyId.Value) is not IAM iam) { // get iam, otherwise register it
                                iam = IAM.Create(iamArn, iamUserName, accessKeyId.Value, accessSecret.Value, command.rotationIntervalInDays);
                                account.AddIdentity(iam);
                            }
                            if(iam.Rotations.FirstOrDefault(x => x.CodeRepositoryId == codeRepo.Id && x.Environment == envName && x.AccessKeyIdVariableName == accessKeyId.Key && x.AccessSecretVariableName == accessSecret.Key) is not Rotation rotation) // get rotation, otherwise register it
                            {
                                rotation = Rotation.Create(envName, accessKeyId.Key, accessSecret.Key, codeRepo.Id);
                                iam.AddRotation(rotation);
                            }
                            _accountsRepository.Update(account);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    
                }
            }



            //if (allProjects.SingleOrDefault(x => x.WebUrl == command.url) is not Project project)
            //{
            //    return Domain.Common.Errors.Gitlab.DoesNotExist;
            //}



            return true;
        }
    }
}
