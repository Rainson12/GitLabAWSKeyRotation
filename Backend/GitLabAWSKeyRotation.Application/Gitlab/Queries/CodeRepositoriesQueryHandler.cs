using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.Gilab.Queries
{

    public class CodeRepositoriesQueryHandler : IRequestHandler<CodeRepositoriesQuery, ErrorOr<List<Domain.GitLab.CodeRepository>>>
    {
        private readonly IGitlabAccessTokenRepository _accessTokenRepository;

        public CodeRepositoriesQueryHandler(IGitlabAccessTokenRepository accessTokenRepository)
        {
            _accessTokenRepository = accessTokenRepository;
        }

        public async Task<ErrorOr<List<Domain.GitLab.CodeRepository>>> Handle(CodeRepositoriesQuery query, CancellationToken cancellationToken)
        {
            return await _accessTokenRepository.GetCodeRepositories(query.accessToken);
        }
    }
}
