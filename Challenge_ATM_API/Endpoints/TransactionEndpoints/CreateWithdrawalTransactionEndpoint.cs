using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.TransactionEndpoints
{
    [Tags("Transaction")]
    public class CreateWithdrawalTransactionEndpoint : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public CreateWithdrawalTransactionEndpoint(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpPost("transaction/createWithdrawal")]
        [SwaggerOperation(
            Summary = "Create Withdrawal",
            Description = "Withdrawal money to account",
            OperationId = "CreateWithdrawal"
            )
        ]
        public async Task<ActionResult<int>> CreateWithdrawal([FromBody] double amount)
        {
            try
            {
                var number = User.Identity?.Name;
                if (string.IsNullOrEmpty(number))
                    return Unauthorized("Token inválido o sin número de tarjeta.");
                return Ok(await _transactionService.CreateWithdrawalTransaction(amount, number));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
