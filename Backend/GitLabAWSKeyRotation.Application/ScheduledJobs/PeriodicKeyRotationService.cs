using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using GitLabApiClient;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;

namespace GitLabAWSKeyRotation.Application.ScheduledJobs
{


    internal class PeriodicKeyRotationService : IPeriodicTask
    {
        IAccountsRepository _accountsRepository;
        ICodeRepositoryRepository _codeRepositoryRepository;
        public PeriodicKeyRotationService(IAccountsRepository accountRepository, ICodeRepositoryRepository codeRepositoryRepository)
        {
            _accountsRepository = accountRepository;
            _codeRepositoryRepository = codeRepositoryRepository;
        }

        public async Task DoWork()
        {
            var accounts = await _accountsRepository.GetAll();
            var codeRepositories = await _codeRepositoryRepository.GetAll();
            foreach (var account in accounts)
            {
                foreach (var iam in account.IamIdentities)
                {
                    try
                    {
                        var iamClient = new AmazonIdentityManagementServiceClient(iam.AccessKeyId, iam.AccessSecret);
                        var keys = await iamClient.ListAccessKeysAsync(new ListAccessKeysRequest { UserName = iam.Name });

                        var currentKey = keys.AccessKeyMetadata.First(x => x.AccessKeyId == iam.AccessKeyId);
                        if (iam.KeyRotationInDays != 0 && currentKey.CreateDate < DateTime.Now.Subtract(new TimeSpan(0, Convert.ToInt32(iam.KeyRotationInDays * 24 * 60), 0))) // is key outdated
                        {
                            var newKey = await iamClient.CreateAccessKeyAsync(new CreateAccessKeyRequest { UserName = iam.Name });
                            await iamClient.DeleteAccessKeyAsync(new() { AccessKeyId = iam.AccessKeyId, UserName = iam.Name });

                            iam.UpdateKey(newKey.AccessKey.AccessKeyId, newKey.AccessKey.SecretAccessKey);
                            _accountsRepository.Update(account);
                            foreach (var rotation in iam.Rotations)
                            {
                                var codeRepo = codeRepositories.First(x => x.Id == rotation.CodeRepositoryId);

                                Uri gitlabRepoWebUrl = new Uri(codeRepo.Url);
                                var gitlabRootUrl = gitlabRepoWebUrl.GetLeftPart(UriPartial.Authority);
                                var gitlabClient = new GitLabClient(gitlabRootUrl, codeRepo.AccessKey.Token);
                                var allProjects = await gitlabClient.Projects.GetAsync();
                                var project = allProjects.Single(x => x.WebUrl == codeRepo.Url);

                                await gitlabClient.Projects.UpdateVariableAsync(project.Id, new() { Key = rotation.AccessKeyIdVariableName, EnvironmentScope = rotation.Environment, Value = newKey.AccessKey.AccessKeyId  });
                                await gitlabClient.Projects.UpdateVariableAsync(project.Id, new() { Key = rotation.AccessSecretVariableName, EnvironmentScope = rotation.Environment, Value = newKey.AccessKey.SecretAccessKey });
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                _accountsRepository.Update(account);
            }
        }
    }
}
