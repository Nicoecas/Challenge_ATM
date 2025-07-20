using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.TransactionEndpoints
{
    [Tags("Transaction")]
    public class GetBalanceTransactionEndpoint : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public GetBalanceTransactionEndpoint(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpGet("transaction/getBalance")]
        [SwaggerOperation(
            Summary = "Get Balance",
            Description = "Get a Balance and add a transaction register",
            OperationId = "GetBalance"
            )
        ]
        public async Task<ActionResult<GetBalanceDtoResponse>> GetBalance()
        {
            try
            {
                var number = User.Identity?.Name;
                if (string.IsNullOrEmpty(number))
                    return Unauthorized("Token inválido o sin número de tarjeta.");

                return Ok(await _transactionService.GetBalanceTransaction(number));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


    public class GetBalanceDtoResponse
    {
        public string Number { get; set; } = string.Empty;
        public double Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
