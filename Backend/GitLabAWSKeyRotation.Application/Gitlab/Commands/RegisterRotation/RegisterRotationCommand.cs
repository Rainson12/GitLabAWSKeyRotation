using MediatR;
using ErrorOr;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{

    public record RegisterRotationCommand(string environment, string accessKeyIdVariableName, string accessSecretVariableName, Guid CodeRepositoryId, Guid IamId, Guid AwsAccountId) : IRequest<ErrorOr<Domain.GitLab.Rotation>>;
}
