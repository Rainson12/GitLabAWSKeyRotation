using GitLabAWSKeyRotation.Domain.GitLab;

namespace GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance
{
    public  interface ICodeRepositoryRepository
    {
        bool Exists(Guid repositoryId);
        bool ExistsByUrl(string url);
        CodeRepository? Get(Guid repositoryId);
        Task<List<CodeRepository>> GetAll();
        CodeRepository Update(CodeRepository account);
        void Add(CodeRepository account);
    }
}
