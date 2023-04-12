using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Queries
{

    public record IAMQuery(IAMId id) : IRequest<ErrorOr<Domain.AWS.Entities.IAM>>;
}
