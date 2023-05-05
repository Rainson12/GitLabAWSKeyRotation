using MediatR;
using ErrorOr;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Authentication;

namespace GitLabAWSKeyRotation.Application.Authentication.Commands.Queries
{
    public class IsConfiguredQueryHandler : IRequestHandler<IsConfiguredQuery, ErrorOr<bool>>
    {
        IApplicationRepository _applicationRepository { get; set; }

        public IsConfiguredQueryHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<ErrorOr<bool>> Handle(IsConfiguredQuery query, CancellationToken cancellationToken)
        {
            return await _applicationRepository.IsConfigured();
        }
    }
}
