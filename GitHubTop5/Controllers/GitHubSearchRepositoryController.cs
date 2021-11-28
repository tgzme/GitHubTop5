using GitHubTop5.DomainModels;
using GitHubTop5.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GitHubTop5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubSearchRepositoryController : ControllerBase
    {
        private readonly IGitHubSearchRepositoryService _gitHubSearchRepositoryService;

        public GitHubSearchRepositoryController(IGitHubSearchRepositoryService gitHubSearchRepositoryService)
        {
            _gitHubSearchRepositoryService = gitHubSearchRepositoryService;
        }

        [Route("GetTop5Starred")]
        [HttpGet]
        public async Task<Root> GetTop5Starred(string language)
        {
            var ret = await _gitHubSearchRepositoryService.GetTop5Starred(language);
           
            return ret;
        }
    }
}
