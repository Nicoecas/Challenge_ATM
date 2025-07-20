using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.CardEndpoints
{
    [Tags("Card")]
    public class ValidateCardEndpoint : ControllerBase
    {
        private readonly ICardService _cardService;

        public ValidateCardEndpoint(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("card/validateCard")]
        [SwaggerOperation(
            Summary = "Validate Card",
            Description = "Recibed a number of card and PIN, if dont exist or is error, the system charge a attemp, 4 times and card'll locked.",
            OperationId = "ValidateCard"
            )
        ]
        public async Task<ActionResult<ValidateCardDtoResponse>> ValidateCard([FromBody] ValidateCardDtoRequest req)
        {
            try
            {
                return Ok(await _cardService.ValidateCard(req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class ValidateCardDtoRequest
    {
        public required string Number { get; set; }
        public required string PIN { get; set; }
    }

    public class ValidateCardDtoResponse
    {
        public bool IsVerify { get; set; } = false;
        public string Token  { get; set; } = string.Empty;
        public int? Attempts { get; set; }
    }
}
