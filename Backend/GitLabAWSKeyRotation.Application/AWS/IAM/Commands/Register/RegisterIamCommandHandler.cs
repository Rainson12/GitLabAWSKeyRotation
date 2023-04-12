using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using Amazon.SecurityToken.Model;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Commands.Register
{

    public class RegisterIamCommandHandler : IRequestHandler<RegisterIamCommand, ErrorOr<Domain.AWS.Entities.IAM>>
    {
        private readonly IAccountsRepository _accountsRepository;

        public RegisterIamCommandHandler(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<ErrorOr<Domain.AWS.Entities.IAM>> Handle(RegisterIamCommand command, CancellationToken cancellationToken)
        {
            if (_accountsRepository.Get(command.accountId) is not Domain.AWS.Account account)
                return Domain.Common.Errors.Account.DoesNotExist;

            string accountArn = "";
            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(command.accessKeyId, command.accessSecret);
                var stsClient = new Amazon.SecurityToken.AmazonSecurityTokenServiceClient(awsCredentials);
                var callerIdRequest = new GetCallerIdentityRequest();
                var result = await stsClient.GetCallerIdentityAsync(callerIdRequest);
                accountArn = result.Arn;
            }
            catch (Exception ex)
            {
                return Domain.Common.Errors.IAM.InvalidAccessCredentials;
            }

            if (account.IamIdentities.Any(x => x.Arn == accountArn))
            {
                return Domain.Common.Errors.IAM.AlreadyRegistered;
            }

            var iam = Domain.AWS.Entities.IAM.Create(accountArn, command.name, command.accessKeyId, command.accessSecret, command.rotationInDays);
            account.AddIdentity(iam);

            _accountsRepository.Update(account);
            return iam;
        }
    }
}
