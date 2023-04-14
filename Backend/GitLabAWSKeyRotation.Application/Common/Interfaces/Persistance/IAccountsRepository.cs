using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;

namespace GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance
{
    public  interface IAccountsRepository
    {
        bool Exists(string accountId);
        Account? Get(string accountId);
        Task<List<Account>> GetAll();
        Account? GetByUuid(Guid id);
        Account Update(Account account);
        void Add(Account account);
    }
}
