using Challenge_ATM_API.Endpoints.CardEndpoints;
using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Repository;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Challenge_ATM_API.Service
{
    public interface ICardService
    {
        Task<ValidateCardDtoResponse> ValidateCard(ValidateCardDtoRequest card);
        Task<bool> CardExist(string number);
    }
    public class CardService : ICardService
    {
        private readonly IAsyncRepository<Card> _repository;
        private readonly IAuthService _authService;
        public CardService(IAsyncRepository<Card> repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<bool> CardExist(string number)
        {
            if (string.IsNullOrEmpty(number) ||
               number.Length != 16 ||
               !number.All(char.IsDigit))
            {
                throw new Exception("Número de la tarjeta invalido");
            }

            return (await _repository.GetSet().FirstOrDefaultAsync(x => x.Number == number)) is not null;
        }

        public async Task<ValidateCardDtoResponse> ValidateCard(ValidateCardDtoRequest cardReq)
        {
            var response = new ValidateCardDtoResponse();
            var card = await _repository.GetSet().FirstOrDefaultAsync(x => x.Number == cardReq.Number);
            if (card == null)
            {
                throw new FileNotFoundException("Tarjeta no encontrada");
            }
            if (card.IsLocked)
            {
                throw new InvalidOperationException("Tarjeta bloqueada, llame a administración");
            }

            var token = await _authService.LoginAsync(card, cardReq.PIN);

            if (token == null)
            {
                card.Attempts+=1;
                card.IsLocked = card.Attempts == 4 ? true : false;
                response = new ValidateCardDtoResponse
                {
                    IsVerify = false,
                    Token = "",
                    Attempts = card.Attempts
                };
            }
            else
            {
                card.Attempts = 0;
                response = new ValidateCardDtoResponse
                {
                    IsVerify = true,
                    Token = token
                };
            }
            await _repository.UpdateAsync(card);
            return response;
        }
    }
}
