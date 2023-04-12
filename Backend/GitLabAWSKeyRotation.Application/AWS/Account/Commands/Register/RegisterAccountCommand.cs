using MediatR;
using ErrorOr;

namespace GitLabAWSKeyRotation.Application.AWS.Account.Commands.Register
{

    public record RegisterAccountCommand(string displayName, string accountId) : IRequest<ErrorOr<Domain.AWS.Account>>;
}
