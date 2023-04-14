using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient;
using Microsoft.Extensions.Configuration;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{
    public class RegisterAccessTokenCommandHandler : IRequestHandler<RegisterAccessTokenCommand, ErrorOr<Domain.GitLab.AccessToken>>
    {
        private readonly IGitlabAccessTokenRepository _accessTokenRepository;
        private readonly IConfiguration _config;

        public RegisterAccessTokenCommandHandler(IGitlabAccessTokenRepository accessTokenRepository, IConfiguration config)
        {
            _accessTokenRepository = accessTokenRepository;
            _config = config;
        }

        public async Task<ErrorOr<Domain.GitLab.AccessToken>> Handle(RegisterAccessTokenCommand command, CancellationToken cancellationToken)
        {
            
            if (_accessTokenRepository.ExistsByName(command.name))
                return Domain.Common.Errors.Gitlab.AccessTokenAlreadyRegistered;
            var gitlabUrl = _config.GetSection("Gitlab").GetValue<string>("Url");

            Uri gitlabRepoWebUrl = new Uri(gitlabUrl);
            var gitlabRootUrl = gitlabRepoWebUrl.GetLeftPart(UriPartial.Authority);
            var gitlabClient = new GitLabClient(gitlabRootUrl, command.token);
            var allProjects = await gitlabClient.Projects.GetAsync();

            var codeRepo = Domain.GitLab.AccessToken.Create(command.name, command.token);
            _accessTokenRepository.Add(codeRepo);

            return codeRepo;
        }
    }
}
