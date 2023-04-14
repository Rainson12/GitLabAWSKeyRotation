using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;

namespace GitLabAWSKeyRotation.Application.AWS.IAM.Queries
{

    public record AccountsQuery() : IRequest<ErrorOr<List<Domain.AWS.Account>>>;
}
