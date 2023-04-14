using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Queries
{

    public class AccountsQueryHandler : IRequestHandler<AccountsQuery, ErrorOr<List<Domain.AWS.Account>>>
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsQueryHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<ErrorOr<List<Domain.AWS.Account>>> Handle(AccountsQuery query, CancellationToken cancellationToken)
        {
            return await _accountsRepository.GetAll();
        }
    }
}
