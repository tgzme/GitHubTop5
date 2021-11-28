using Newtonsoft.Json;
using System.Collections.Generic;

namespace GitHubTop5.DomainModels
{
    public class Root
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }

        [JsonProperty("items")]
        public IEnumerable<Item> Items { get; set; }
    }
}
