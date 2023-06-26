using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using GitLabApiClient;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using System.Runtime.CompilerServices;

namespace GitLabAWSKeyRotation.Application.ScheduledJobs
{


    internal class PeriodicKeyRotationService : IPeriodicTask
    {
        IAccountsRepository _accountsRepository;
        IGitlabAccessTokenRepository _accessTokenRepository;
        public PeriodicKeyRotationService(IAccountsRepository accountRepository, IGitlabAccessTokenRepository codeRepositoryRepository)
        {
            _accountsRepository = accountRepository;
            _accessTokenRepository = codeRepositoryRepository;
        }

        public async Task DoWork()
        {
            var accounts = await _accountsRepository.GetAllWithAllSubProperties();
            var accessTokens = await _accessTokenRepository.GetAllWithAllSubProperties();
            var repos = accessTokens.SelectMany(x => x.CodeRepositories, (accessToken, CodeRepository) => new { accessToken.Token, CodeRepository }).ToList();

            foreach (var account in accounts)
            {
                foreach (var iam in account.IamIdentities)
                {
                    try
                    {
                        var iamClient = new AmazonIdentityManagementServiceClient(iam.AccessKeyId, iam.AccessSecret);
                        var keys = await iamClient.ListAccessKeysAsync(new ListAccessKeysRequest { UserName = iam.Name });

                        var currentKey = keys.AccessKeyMetadata.First(x => x.AccessKeyId == iam.AccessKeyId);
                        if (iam.KeyRotationInDays != 0 && currentKey.CreateDate < DateTime.Now.Subtract(new TimeSpan(0, Convert.ToInt32(iam.KeyRotationInDays * 24 * 60), 0))) // is key outdated
                        {
                            var newKey = await iamClient.CreateAccessKeyAsync(new CreateAccessKeyRequest { UserName = iam.Name });
                            await iamClient.DeleteAccessKeyAsync(new() { AccessKeyId = iam.AccessKeyId, UserName = iam.Name });

                            iam.UpdateKey(newKey.AccessKey.AccessKeyId, newKey.AccessKey.SecretAccessKey);
                            _accountsRepository.Update(account);

                            // update all gitlab variables
                            foreach (var rotation in iam.Rotations)
                            {
                                var codeRepoWithAccessToken = repos.FirstOrDefault(x => x.CodeRepository.Id == rotation.CodeRepositoryId);
                                
                                if (codeRepoWithAccessToken == null)
                                    continue;
                                
                                Uri gitlabRepoWebUrl = new Uri(codeRepoWithAccessToken.CodeRepository.Url);
                                var gitlabRootUrl = gitlabRepoWebUrl.GetLeftPart(UriPartial.Authority);
                                var gitlabClient = new GitLabClient(gitlabRootUrl, codeRepoWithAccessToken.Token);
                                var allProjects = await gitlabClient.Projects.GetAsync();
                                var project = allProjects.Single(x => x.WebUrl == codeRepoWithAccessToken.CodeRepository.Url);

                                await gitlabClient.Projects.UpdateVariableAsync(project.Id, new() { Key = rotation.AccessKeyIdVariableName, EnvironmentScope = rotation.Environment, Value = newKey.AccessKey.AccessKeyId });
                                await gitlabClient.Projects.UpdateVariableAsync(project.Id, new() { Key = rotation.AccessSecretVariableName, EnvironmentScope = rotation.Environment, Value = newKey.AccessKey.SecretAccessKey });
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                _accountsRepository.Update(account);
            }
        }
    }
}
