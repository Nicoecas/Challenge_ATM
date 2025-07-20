using Challenge_ATM_API.Endpoints.BankEndpoints;
using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Repository;

namespace Challenge_ATM_API.Service
{
    public interface IBankService
    {

        Task<int> CreateBank(Bank bank);
    }
    public class BankService : IBankService
    {
        private readonly IAsyncRepository<Bank> _repository;
        public BankService(IAsyncRepository<Bank> repository)
        {
            _repository = repository;
        }
        public async Task<int> CreateBank(Bank bank)
        {
            var bankBD = await _repository.AddAsync(bank);
            return bankBD.Id;
        }
    }
}
