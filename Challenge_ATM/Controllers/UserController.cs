using Challenge_ATM.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Challenge_ATM.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var client = _httpClientFactory.CreateClient("APIClient");

            var token = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("/user/getUser");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = await response.Content.ReadAsStringAsync();
                return View();
            }
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UserModel>(
                responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return View(result);
        }
    }
}
