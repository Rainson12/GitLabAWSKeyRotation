using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.Gilab.Queries
{

    public class RotationsQueryHandler : IRequestHandler<RotationsQuery, ErrorOr<List<Domain.GitLab.Rotation>>>
    {
        private readonly IGitlabAccessTokenRepository _accessTokenRepository;

        public RotationsQueryHandler(IGitlabAccessTokenRepository accessTokenRepository)
        {
            _accessTokenRepository = accessTokenRepository;
        }

        public async Task<ErrorOr<List<Domain.GitLab.Rotation>>> Handle(RotationsQuery query, CancellationToken cancellationToken)
        {
            return await _accessTokenRepository.GetRotations(query.repositoryId);
        }
    }
}
