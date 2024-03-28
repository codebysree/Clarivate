using Clarivate.Models.Entity;
using Clarivate.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Clarivate.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7205/api/");
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserModel userModel)
        {
            try
            {
                var apiResponse = await GetUserInfo(userModel);
                if (apiResponse != null)
                {
                    return View("LoggedInView", apiResponse);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(userModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(userModel);
            }
        }
        private async Task<Result> GetUserInfo(UserModel userModel)
        {
            var response = await _httpClient.PostAsJsonAsync("user/login", userModel);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Result>();
            }
            else
            {
                return null;
            }
        }

    }
}
