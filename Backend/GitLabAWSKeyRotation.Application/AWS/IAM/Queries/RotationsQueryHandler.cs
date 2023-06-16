using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.AWS.Queries
{

    public class RotationsQueryHandler : IRequestHandler<RotationsQuery, ErrorOr<List<Domain.GitLab.Rotation>>>
    {
        private readonly IAccountsRepository _accountsRepository;

        public RotationsQueryHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<ErrorOr<List<Domain.GitLab.Rotation>>> Handle(RotationsQuery query, CancellationToken cancellationToken)
        {
            return await _accountsRepository.GetRotations(query.iamId);
        }
    }
}
