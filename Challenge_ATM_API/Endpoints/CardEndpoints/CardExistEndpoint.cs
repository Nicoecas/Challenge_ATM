using AutoMapper;
using Challenge_ATM_API.Endpoints.BankEndpoints;
using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.CardEndpoints
{
    [Tags("Card")]
    public class CardExistEndpoint : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardExistEndpoint(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("card/cardExist")]
        [SwaggerOperation(
            Summary = "Create Card",
            Description = "Recibed a number of card. If exist, response true.",
            OperationId = "CardExist"
            )
        ]
        public async Task<ActionResult<bool>> ValidateCard([FromBody] string number)
        {
            try
            {
                return Ok(await _cardService.CardExist(number));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
