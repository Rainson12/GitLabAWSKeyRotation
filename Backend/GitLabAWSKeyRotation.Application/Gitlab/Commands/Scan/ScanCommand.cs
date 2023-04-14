using MediatR;
using ErrorOr;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{

    public record ScanCommand(string name, string token, float rotationIntervalInDays) : IRequest<ErrorOr<bool>>;
}
