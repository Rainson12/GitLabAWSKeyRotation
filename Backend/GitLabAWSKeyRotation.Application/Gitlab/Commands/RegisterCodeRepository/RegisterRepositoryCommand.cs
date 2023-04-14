using MediatR;
using ErrorOr;

namespace GitLabAWSKeyRotation.Application.Gitlab.Commands.RegisterRotation
{

    public record RegisterRepositoryCommand(Guid accessTokenId, string url, string name) : IRequest<ErrorOr<Domain.GitLab.CodeRepository>>;
}
