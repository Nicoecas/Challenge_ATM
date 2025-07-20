using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Challenge_ATM.Models;
using Challenge_ATM.DTOs;
using System.Reflection;

namespace Challenge_ATM.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VerifyCardModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("APIClient");
            var content = new StringContent(JsonSerializer.Serialize(model.Number1 + model.Number2 + model.Number3 + model.Number4), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/card/cardExist", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = await response.Content.ReadAsStringAsync();
                return View(model);
            }
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<bool>(
                responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            if (!result)
            {
                TempData["Message"] = "La tarjeta no existe";
                return View(model);
            }

            return RedirectToAction("VerifyCard", "Auth", new
            {
                number1 = model.Number1,
                number2 = model.Number2,
                number3 = model.Number3,
                number4 = model.Number4
            });
        }

        [HttpGet]
        [Route("/Auth/VerifyCard")]
        public IActionResult VerifyCard(
            [FromQuery] string number1,
            [FromQuery] string number2,
            [FromQuery] string number3,
            [FromQuery] string number4)
        {
            ViewData["Number1"] = number1;
            ViewData["Number2"] = number2;
            ViewData["Number3"] = number3;
            ViewData["Number4"] = number4;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCard(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ViewData["Number1"] = model.Number1;
            ViewData["Number2"] = model.Number2;
            ViewData["Number3"] = model.Number3;
            ViewData["Number4"] = model.Number4;
            var client = _httpClientFactory.CreateClient("APIClient");
            CardValidationDtoRequest cardValidationDtoRequest = new CardValidationDtoRequest
            {
                PIN = model.PIN,
                Number = model.Number1 + model.Number2 + model.Number3 + model.Number4,
            };
            var content = new StringContent(JsonSerializer.Serialize(cardValidationDtoRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/card/validateCard", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = await response.Content.ReadAsStringAsync();
                return View(model);
            }
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ValidateCardDtoResponse>(
                responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            if (result!.IsVerify && !string.IsNullOrEmpty(result.Token))
            {
                Response.Cookies.Append("jwt", result.Token!, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, cardValidationDtoRequest.Number) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
            }

            TempData["Message"] = result.Attempts != 4 ? $"Datos incorrectos, Intentos: {result.Attempts}/4." : $"Datos incorrectos, TARJETA BLOQUEADA.";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Auth");
        }
    }
}
