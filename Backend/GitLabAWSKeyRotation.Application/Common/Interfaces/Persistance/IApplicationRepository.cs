using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;

namespace GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance
{
    public  interface IApplicationRepository
    {
        Task<Domain.Application.Application> Create(Domain.Application.Application app);
        Task<Domain.Application.Application> Update(Domain.Application.Application app);
        Task<Domain.Application.Application> Get();
        Task<bool> IsConfigured();
    }
}
