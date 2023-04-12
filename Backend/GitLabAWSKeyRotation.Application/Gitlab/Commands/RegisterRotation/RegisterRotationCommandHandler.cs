using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabApiClient;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{
    public class RegisterRotationCommandHandler : IRequestHandler<RegisterRotationCommand, ErrorOr<Domain.GitLab.Rotation>>
    {
        private readonly ICodeRepositoryRepository _codeRepositoryRepository;
        private readonly IAccountsRepository _accountsRepository;

        public RegisterRotationCommandHandler(ICodeRepositoryRepository codeRepositoryRepository, IAccountsRepository accountsRepository)
        {
            _codeRepositoryRepository = codeRepositoryRepository;
            _accountsRepository = accountsRepository;
        }

        public async Task<ErrorOr<Domain.GitLab.Rotation>> Handle(RegisterRotationCommand command, CancellationToken cancellationToken)
        {
            if (_codeRepositoryRepository.Get(command.CodeRepositoryId) is not Domain.GitLab.CodeRepository _codeRepo)
                return Domain.Common.Errors.Rotation.DoesNotExist;

            var rotation = Domain.GitLab.Rotation.Create(command.environment, command.accessKeyIdVariableName, command.accessSecretVariableName, _codeRepo.Id);

            if(_accountsRepository.GetByUuid(command.AwsAccountId) is not Account account)
                return Domain.Common.Errors.Account.DoesNotExist;
            if(account.IamIdentities.FirstOrDefault(x => x.Id.Value == command.IamId) is not IAM iam) 
                return Domain.Common.Errors.IAM.DoesNotExist;


            Uri gitlabRepoWebUrl = new Uri(_codeRepo.Url);
            var gitlabRootUrl = gitlabRepoWebUrl.GetLeftPart(UriPartial.Authority);
            var gitlabClient = new GitLabClient(gitlabRootUrl, _codeRepo.AccessKey.Token);
            var allProjects = await gitlabClient.Projects.GetAsync();
            if(allProjects.SingleOrDefault(x => x.WebUrl == _codeRepo.Url) is not Project project)
            {
                return Domain.Common.Errors.Gitlab.DoesNotExist;
            }

            var projectVariables = await gitlabClient.Projects.GetVariablesAsync(project.Id);
            if(!projectVariables.Any(x => x.EnvironmentScope == command.environment && x.Key == command.accessKeyIdVariableName))
                return Domain.Common.Errors.Rotation.AccessKeyIdVariableDoesNotExist;
            if (!projectVariables.Any(x => x.EnvironmentScope == command.environment && x.Key == command.accessSecretVariableName))
                return Domain.Common.Errors.Rotation.AccessSecretVariableDoesNotExist;

            iam.AddRotation(rotation);
            _accountsRepository.Update(account);

            return rotation;
        }
    }
}
