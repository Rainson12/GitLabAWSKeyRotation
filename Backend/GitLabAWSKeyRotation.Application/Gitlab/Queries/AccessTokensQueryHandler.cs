using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.Gilab.Queries
{

    public class AccessTokensQueryHandler : IRequestHandler<AccessTokensQuery, ErrorOr<List<Domain.GitLab.AccessToken>>>
    {
        private readonly IGitlabAccessTokenRepository _accessTokenRepository;

        public AccessTokensQueryHandler(IGitlabAccessTokenRepository accessTokenRepository)
        {
            _accessTokenRepository = accessTokenRepository;
        }

        public async Task<ErrorOr<List<Domain.GitLab.AccessToken>>> Handle(AccessTokensQuery query, CancellationToken cancellationToken)
        {
            return await _accessTokenRepository.GetAll();
        }
    }
}
