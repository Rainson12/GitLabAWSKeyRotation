using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{
    public class RegisterRepositoryCommandHandler : IRequestHandler<RegisterRepositoryCommand, ErrorOr<Domain.GitLab.CodeRepository>>
    {
        private readonly ICodeRepositoryRepository _codeRepositoryRepository;

        public RegisterRepositoryCommandHandler(ICodeRepositoryRepository codeRepositoryRepository)
        {
            _codeRepositoryRepository = codeRepositoryRepository;
        }

        public async Task<ErrorOr<Domain.GitLab.CodeRepository>> Handle(RegisterRepositoryCommand command, CancellationToken cancellationToken)
        {
            if (_codeRepositoryRepository.ExistsByUrl(command.url))
                return Domain.Common.Errors.Gitlab.AlreadyRegistered;

            Uri gitlabRepoWebUrl = new Uri(command.url);
            var gitlabRootUrl = gitlabRepoWebUrl.GetLeftPart(UriPartial.Authority);
            var gitlabClient = new GitLabClient(gitlabRootUrl, command.token);
            var allProjects = await gitlabClient.Projects.GetAsync();
            if (allProjects.SingleOrDefault(x => x.WebUrl == command.url) is not Project project)
            {
                return Domain.Common.Errors.Gitlab.DoesNotExist;
            }

            var accessKey = AccessKey.Create(command.identifier, command.token);
            var codeRepo = Domain.GitLab.CodeRepository.Create(command.url, accessKey);
            _codeRepositoryRepository.Add(codeRepo);

            return codeRepo;
        }
    }
}
