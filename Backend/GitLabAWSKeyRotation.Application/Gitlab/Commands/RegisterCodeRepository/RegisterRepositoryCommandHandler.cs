using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient;
using GitLabAWSKeyRotation.Domain.GitLab;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{
    public class RegisterRepositoryCommandHandler : IRequestHandler<RegisterRepositoryCommand, ErrorOr<Domain.GitLab.CodeRepository>>
    {
        private readonly IGitlabAccessTokenRepository _accessTokenRepository;

        public RegisterRepositoryCommandHandler(IGitlabAccessTokenRepository accessTokenRepository)
        {
            _accessTokenRepository = accessTokenRepository;
        }

        public async Task<ErrorOr<Domain.GitLab.CodeRepository>> Handle(RegisterRepositoryCommand command, CancellationToken cancellationToken)
        {
            if (_accessTokenRepository.Get(command.accessTokenId) is not AccessToken token)
                return Domain.Common.Errors.Gitlab.RepositoryDoesNotExist;

            Uri gitlabRepoWebUrl = new Uri(command.url);
            var gitlabRootUrl = gitlabRepoWebUrl.GetLeftPart(UriPartial.Authority);
            var gitlabClient = new GitLabClient(gitlabRootUrl, token.Token);
            var allProjects = await gitlabClient.Projects.GetAsync();
            if (allProjects.SingleOrDefault(x => x.WebUrl == command.url) is not Project project)
            {
                return Domain.Common.Errors.Gitlab.RepositoryDoesNotExist;
            }

            var codeRepo = Domain.GitLab.CodeRepository.Create(command.url, command.name);
            token.AddCodeRepository(codeRepo);
            _accessTokenRepository.Update(token);

            return codeRepo;
        }
    }
}
