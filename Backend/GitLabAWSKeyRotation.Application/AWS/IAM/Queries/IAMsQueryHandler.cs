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
            if (_accountsRepository.GetByUuid(query.accountId.Value) is not Domain.AWS.Account account)
                return Domain.Common.Errors.Account.DoesNotExist;
            return account.IamIdentities.ToList();
        }
    }
}
