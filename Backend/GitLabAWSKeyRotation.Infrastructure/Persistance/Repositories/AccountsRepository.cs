using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly GitLabAWSKeyRotationDbContext _dbContext;

        public AccountsRepository(GitLabAWSKeyRotationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Account account)
        {
            _dbContext.Add(account);
            _dbContext.SaveChanges();
        }

        public bool Exists(string accountId)
        {
            return _dbContext.Accounts.Any(x => x.AwsAccountId == accountId);
        }

        public Account? Get(string accountId)
        {
            return _dbContext.Accounts
                .AsSplitQuery()
                .Include(x => x.IamIdentities)
                .ThenInclude(y => y.Rotations).FirstOrDefault(x => x.AwsAccountId == accountId);
        }

        public Task<List<Account>> GetAll()
        {
            return _dbContext.Accounts
                .AsSplitQuery()
                .Include(x => x.IamIdentities)
                .ThenInclude(y => y.Rotations).ToListAsync();
        }

        public Account? GetByUuid(Guid id)
        {
            return _dbContext.Accounts
                .AsSplitQuery()
                .Include(x => x.IamIdentities)
            .ThenInclude(y => y.Rotations).FirstOrDefault(x => x.Id == AccountId.Create(id));
        }

        public Account Update(Account account)
        {
            _dbContext.Update(account);
            _dbContext.SaveChanges();
            return account;
        }
    }
}
