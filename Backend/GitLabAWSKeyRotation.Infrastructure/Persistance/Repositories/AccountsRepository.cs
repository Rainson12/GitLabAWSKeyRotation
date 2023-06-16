using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.GitLab;
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
            return _dbContext.Accounts.ToListAsync();
        }

        public async Task<List<IAM>> GetIAMs(AccountId accountId)
        {
            var account = await _dbContext.Accounts.Include(x => x.IamIdentities).FirstOrDefaultAsync(x => x.Id == accountId);
            return account?.IamIdentities.ToList() ?? new List<IAM>();
        }

        public async Task<List<Rotation>> GetRotations(IAMId iamId)
        {
            var iam = await _dbContext.IAMs.Include(x => x.Rotations).FirstOrDefaultAsync(x => x.Id == iamId);
            return iam?.Rotations.ToList() ?? new List<Rotation>();
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
