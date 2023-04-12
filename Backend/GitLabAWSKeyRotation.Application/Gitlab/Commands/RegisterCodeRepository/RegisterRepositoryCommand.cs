using MediatR;
using ErrorOr;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{

    public record RegisterRepositoryCommand(string url, string identifier, string token) : IRequest<ErrorOr<Domain.GitLab.CodeRepository>>;
}
