using Clarivate.Models.Entity;
using Newtonsoft.Json;

namespace Clarivate.Services
{

    public interface IRandomUserService
    {
        Task<Result> GetRandomUser();
    }
    public class RandomUserService : IRandomUserService
    {
        private readonly HttpClient _httpClient;
        public RandomUserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://randomuser.me/api/");
        }
        public async Task<Result> GetRandomUser()
        {
            Result result = null;

            HttpResponseMessage response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<RandomUserJsonModel>(json);
                if (root != null && root.results.Count > 0)
                {
                    result = root.results[0];
                }
            }

            return result;
        }
    }
}
