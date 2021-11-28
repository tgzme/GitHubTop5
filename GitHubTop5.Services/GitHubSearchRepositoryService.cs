using GitHubTop5.DomainModels;
using GitHubTop5.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubTop5.Services
{
    public class GitHubSearchRepositoryService : IGitHubSearchRepositoryService
    {
        // In a production environment these values would come from non-embedded configuration such as appsettings.json 
        private readonly HttpClient _httpClient;

        public GitHubSearchRepositoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.github.com/search/repositories");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("GitHubTop5");

        }

        public async Task<Root> GetTop5Starred(string language)
        {
            if (!string.IsNullOrWhiteSpace(language))
            {
                var response = await _httpClient.GetAsync($"?q=language:{language}&sort=stars&order=desc&per_page=5");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Root>(content);

                    return data;
                }
            }

            return null;
            
        }
    }
}
