using MediatR;
using ErrorOr;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Commands.Register
{
    public record RegisterIamCommand(string accountId, string name, string accessKeyId, string accessSecret, float rotationInDays) : IRequest<ErrorOr<Domain.AWS.Entities.IAM>>;
}
