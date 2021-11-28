using GitHubTop5.DomainModels;
using System.Threading.Tasks;

namespace GitHubTop5.Services.Interfaces
{
    public interface IGitHubSearchRepositoryService : IService
    {
        Task<Root> GetTop5Starred(string language);
    }
}
