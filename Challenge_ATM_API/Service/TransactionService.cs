using AutoMapper;
using Azure.Core;
using Challenge_ATM_API.Endpoints.TransactionEndpoints;
using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Challenge_ATM_API.Service
{
    public interface ITransactionService
    {
        Task<GetBalanceDtoResponse> GetBalanceTransaction(string numberCard);
        Task<int> CreateWithdrawalTransaction(double amount, string numberCard);
        Task<int> CreateDepositTransaction(double amount, string numberCard);
        Task<List<TransactionDtoResponse>> GetTransactionsByCardNumber(string cardNumber);
    }
    public class TransactionService : ITransactionService
    {
        private readonly IAsyncRepository<Transaction> _repository;
        private readonly IAsyncRepository<Card> _cardRepository;
        private readonly IMapper _mapper;
        public TransactionService(IAsyncRepository<Transaction> repository,
            IAsyncRepository<Card> cardRepository,
            IMapper mapper)
        {
            _repository = repository;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }
        public async Task<GetBalanceDtoResponse> GetBalanceTransaction(string numberCard)
        {
            if (string.IsNullOrEmpty(numberCard) ||
                numberCard.Length != 16 ||
                !numberCard.All(char.IsDigit))
            {
                throw new Exception("Número de la tarjeta invalido");
            }

            var card = await _cardRepository.GetSet().FirstOrDefaultAsync(x => !x.IsLocked && x.Number == numberCard);
            if (card == null)
            {
                throw new Exception("La tarjeta no existe");
            }
            if (card.ExpirationDate <= DateTime.UtcNow)
            {
                throw new Exception("La tarjeta está vencida");
            }

            Transaction transaction = new Transaction
            {
                CardId = card.Id,
                Code = new Guid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Type = TransactionType.Balance
            };
            var transactionBD = await _repository.AddAsync(transaction);

            GetBalanceDtoResponse getBalanceDtoResponse = _mapper.Map<GetBalanceDtoResponse>(card);
            return getBalanceDtoResponse;
        }

        public async Task<int> CreateWithdrawalTransaction(double amount, string numberCard)
        {

            if (amount < 0)
            {
                throw new InvalidOperationException("El monto de extracción debe ser mayor a cero.");
            }
            if (string.IsNullOrEmpty(numberCard) ||
                numberCard.Length != 16 ||
                !numberCard.All(char.IsDigit))
            {
                throw new Exception("Número de la tarjeta invalido");
            }

            var card = await _cardRepository.GetSet().FirstOrDefaultAsync(x => !x.IsLocked && x.Number == numberCard);
            if (card == null)
            {
                throw new Exception("La tarjeta no existe");
            }
            if (card.ExpirationDate <= DateTime.UtcNow)
            {
                throw new Exception("La tarjeta está vencida");
            }
            if (card.Balance - amount < 0)
            {
                throw new InvalidOperationException("El monto excede el saldo disponible.");
            }

            Transaction transaction = new Transaction
            {
                CardId = card.Id,
                Code = new Guid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Type = TransactionType.Withdrawal,
                Amount = amount,
            };

            card.Balance = card.Balance - amount;

            var transactionBD = await _repository.AddAsync(transaction);

            return transactionBD.Id;
        }
        public async Task<int> CreateDepositTransaction(double amount, string numberCard)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("El monto de depósito debe ser mayor a cero.");
            }

            if (string.IsNullOrEmpty(numberCard) ||
                numberCard.Length != 16 ||
                !numberCard.All(char.IsDigit))
            {
                throw new Exception("Número de la tarjeta invalido");
            }

            var card = await _cardRepository.GetSet().FirstOrDefaultAsync(x => !x.IsLocked && x.Number == numberCard);
            if (card == null)
            {
                throw new Exception("La tarjeta no existe");
            }
            if (card.ExpirationDate <= DateTime.UtcNow)
            {
                throw new Exception("La tarjeta está vencida");
            }

            Transaction transaction = new Transaction
            {
                CardId = card.Id,
                Code = new Guid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Type = TransactionType.Deposit,
                Amount = amount,
            };

            card.Balance = card.Balance + amount;

            var transactionBD = await _repository.AddAsync(transaction);

            return transactionBD.Id;
        }


        public async Task<List<TransactionDtoResponse>> GetTransactionsByCardNumber(string cardNumber)
        {

            if (string.IsNullOrEmpty(cardNumber) ||
                cardNumber.Length != 16 ||
                !cardNumber.All(char.IsDigit))
            {
                throw new Exception("Número de la tarjeta invalido");
            }

            var card = await _cardRepository.GetSet().Include(x => x.Transactions).FirstOrDefaultAsync(x => !x.IsLocked && x.Number == cardNumber);
            if (card == null)
            {
                throw new Exception("La tarjeta no existe");
            }


            List<TransactionDtoResponse> getBalanceDtoResponse = _mapper.Map<List<TransactionDtoResponse>>(card.Transactions.OrderByDescending(x => x.CreatedDate));
            return getBalanceDtoResponse;
        }
    }

}
