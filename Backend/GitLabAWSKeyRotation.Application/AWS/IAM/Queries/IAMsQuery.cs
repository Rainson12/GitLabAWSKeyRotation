using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Queries
{

    public record IAMsQuery(AccountId accountId) : IRequest<ErrorOr<List<Domain.AWS.Entities.IAM>>>;
}
