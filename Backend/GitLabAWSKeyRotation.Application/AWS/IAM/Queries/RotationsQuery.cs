using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

namespace GitLabAWSKeyRotation.Application.AWS.Queries
{

    public record RotationsQuery(IAMId iamId) : IRequest<ErrorOr<List<Domain.GitLab.Rotation>>>;
}
