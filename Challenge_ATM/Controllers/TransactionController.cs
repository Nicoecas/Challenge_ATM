using Challenge_ATM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Challenge_ATM.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Balance()
        {
            var client = Authorization();
            if (client == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var response = await client.GetAsync("/transaction/getBalance");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = await response.Content.ReadAsStringAsync();
                return View();
            }
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BalanceModel>(
                responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Transactions()
        {
            var client = Authorization();
            if (client == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var response = await client.GetAsync("/transaction/getTransactions");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = await response.Content.ReadAsStringAsync();
                return View();
            }
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<TransactionModel>>(
                responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return View(result);
        }



        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(DepositModel model)
        {
            var client = Authorization();
            if (client == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var content = new StringContent(JsonSerializer.Serialize(model.Amount), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("transaction/createDeposit", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = await response.Content.ReadAsStringAsync();
                return View(model);
            }

            return RedirectToAction("Transactions", "Transaction");
        }


        [HttpGet]
        public IActionResult Withdrawal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Withdrawal(WithdrawalModel model)
        {
            var client = Authorization();
            if (client == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var content = new StringContent(JsonSerializer.Serialize(model.Amount), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("transaction/createWithdrawal", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = await response.Content.ReadAsStringAsync();
                return View(model);
            }

            return RedirectToAction("Transactions", "Transaction");
        }


        private HttpClient? Authorization()
        {
            var client = _httpClientFactory.CreateClient("APIClient");

            var token = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
                return null;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
