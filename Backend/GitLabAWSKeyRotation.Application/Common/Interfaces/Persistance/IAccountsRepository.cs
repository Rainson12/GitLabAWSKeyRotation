using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.GitLab;

namespace GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance
{
    public  interface IAccountsRepository
    {
        bool Exists(string accountId);
        Account? Get(string accountId);
        Task<List<Account>> GetAll();
        Task<List<IAM>> GetIAMs(AccountId accountId);
        Task<List<Rotation>> GetRotations(IAMId iamId);
        Account? GetByUuid(Guid id);
        Account Update(Account account);
        void Add(Account account);
        Task<List<Account>> GetAllWithAllSubProperties();
    }
}
