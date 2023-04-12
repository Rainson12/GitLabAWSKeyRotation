using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Queries
{

    public class IAMQueryHandler : IRequestHandler<IAMQuery, ErrorOr<Domain.AWS.Entities.IAM>>
    {
        private readonly IAccountsRepository _accountsRepository;

        public IAMQueryHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<ErrorOr<Domain.AWS.Entities.IAM>> Handle(IAMQuery query, CancellationToken cancellationToken)
        {
            // Todo to be implemented
            return default;
        }
    }
}
