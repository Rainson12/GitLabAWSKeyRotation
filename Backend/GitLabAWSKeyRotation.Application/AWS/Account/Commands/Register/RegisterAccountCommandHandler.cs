using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.AWS.Account.Commands.Register
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, ErrorOr<Domain.AWS.Account>>
    {
        private readonly IAccountsRepository _accountsRepository;

        public RegisterAccountCommandHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<ErrorOr<Domain.AWS.Account>> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
        {
            if(_accountsRepository.Exists(command.accountId))
                return Domain.Common.Errors.Account.AlreadyRegistered;

            var account = Domain.AWS.Account.Create(command.accountId, command.displayName);
            _accountsRepository.Add(account);

            return account;
        }
    }
}
