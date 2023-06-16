using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

namespace GitLabAWSKeyRotation.Application.Gilab.Queries
{

    public record CodeRepositoriesQuery(AccessTokenId accessToken) : IRequest<ErrorOr<List<Domain.GitLab.CodeRepository>>>;
}
