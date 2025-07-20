using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.TransactionEndpoints
{
    [Tags("Transaction")]
    public class CreateDepositTransaction : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public CreateDepositTransaction(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpPost("transaction/createDeposit")]
        [SwaggerOperation(
            Summary = "Create Deposit",
            Description = "Add money to account",
            OperationId = "CreateDeposit"
            )
        ]
        public async Task<ActionResult<int>> CreateDeposit([FromBody] double amount)
        {
            try
            {
                var number = User.Identity?.Name;
                if (string.IsNullOrEmpty(number))
                    return Unauthorized("Token inválido o sin número de tarjeta.");

                return Ok(await _transactionService.CreateDepositTransaction(amount, number));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
