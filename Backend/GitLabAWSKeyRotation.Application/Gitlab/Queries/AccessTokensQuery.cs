using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;

namespace GitLabAWSKeyRotation.Application.Gilab.Queries
{

    public record AccessTokensQuery() : IRequest<ErrorOr<List<Domain.GitLab.AccessToken>>>;
}
