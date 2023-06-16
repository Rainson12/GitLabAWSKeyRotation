using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Queries
{

    public class IAMsQueryHandler : IRequestHandler<IAMsQuery, ErrorOr<List<Domain.AWS.Entities.IAM>>>
    {
        private readonly IAccountsRepository _accountsRepository;

        public IAMsQueryHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<ErrorOr<List<Domain.AWS.Entities.IAM>>> Handle(IAMsQuery query, CancellationToken cancellationToken)
        {
            return await _accountsRepository.GetIAMs(query.accountId);
        }
    }
}
