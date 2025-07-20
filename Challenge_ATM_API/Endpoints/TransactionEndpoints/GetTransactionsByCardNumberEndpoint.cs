using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.TransactionEndpoints
{
    [Tags("Transaction")]
    public class GetTransactionsByCardNumberEndpoint : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionsByCardNumberEndpoint(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpGet("transaction/getTransactions")]
        [SwaggerOperation(
            Summary = "Get Transactions",
            Description = "Get all transactions to account",
            OperationId = "GetTransactions"
            )
        ]
        public async Task<ActionResult<List<TransactionDtoResponse>>> GetTransactions()
        {
            try
            {
                var number = User.Identity?.Name;

                if (string.IsNullOrEmpty(number))
                    return Unauthorized("Token inválido o sin número de tarjeta.");
                var transactions = await _transactionService.GetTransactionsByCardNumber(number);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class TransactionDtoResponse
    {
        public int CardId { get; set; }
        public Guid Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public TransactionType Type { get; set; }
        public double? Amount { get; set; }
    }
}
