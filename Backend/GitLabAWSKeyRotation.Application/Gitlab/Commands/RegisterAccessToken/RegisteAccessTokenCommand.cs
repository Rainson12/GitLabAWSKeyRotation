using MediatR;
using ErrorOr;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{

    public record RegisterAccessTokenCommand(string name, string token) : IRequest<ErrorOr<Domain.GitLab.AccessToken>>;
}
