using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PokeApi
{
    public abstract class PokeApiService
    {
        protected static readonly HttpClient client;

        public abstract string CategoryString { get; }

        static PokeApiService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IReadOnlyList<CategoryIndexResult>> GetCategoryIndexResults()
        {
            HttpResponseMessage response = await client.GetAsync(
                $"{CategoryString}/?limit=10000");
            response.EnsureSuccessStatusCode();
            PokeApiCategoryIndex queryResults = await
                response.Content.ReadAsAsync<PokeApiCategoryIndex>();
            return queryResults.Results;
        }

        public async Task<IReadOnlyList<string>> GetCategoryNames()
        {
            IReadOnlyList<CategoryIndexResult> categoryResults = await 
                GetCategoryIndexResults();
            return categoryResults.Select(p => p.Name).ToList();
        }

        public async Task<IReadOnlyList<string>> GetCategoryUrls()
        {
            IReadOnlyList<CategoryIndexResult> categoryResults = await
                GetCategoryIndexResults();
            return categoryResults.Select(p => p.Url).ToList();
        }

        public async Task<JObject> GetById(string id)
        {
            HttpResponseMessage response = await client.GetAsync(
                $"{CategoryString}/{id}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<JObject>(json);
            return obj;
        }
    }
}
