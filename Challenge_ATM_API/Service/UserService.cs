using AutoMapper;
using Challenge_ATM_API.Endpoints.UserEndpoints;
using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Challenge_ATM_API.Service
{
    public interface IUserService
    {
        Task<CreateUserDtoResponse> CreateUser(CreateUserDtoRequest user);
        Task<GetUserDtoResponse> GetUser(string numberCard);
    }
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IAsyncRepository<Card> _cardRepository;
        private readonly IMapper _mapper;
        public UserService(IAsyncRepository<User> repository, IAsyncRepository<Card> cardRepository,
            IMapper mapper)
        {
            _repository = repository;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }
        public async Task<CreateUserDtoResponse> CreateUser(CreateUserDtoRequest req)
        {
            User user = _mapper.Map<User>(req);
            var userBD = await _repository.AddAsync(user);

            var random = new Random();
            var pin = random.Next(0, 10000).ToString("D4");
            string part1 = random.Next(10000000, 99999999).ToString();
            string part2 = random.Next(10000000, 99999999).ToString();
            string number = part1 + part2;
            Card card = new Card
            {
                UserId = user.Id,
                Number = number,
                IsLocked = false,
                PIN = new PasswordHasher<object>().HashPassword(null!, pin),
                Balance = 0,
                ExpirationDate = DateTime.UtcNow.AddYears(5)
            };
            await _cardRepository.AddAsync(card);

            var response = new CreateUserDtoResponse
            {
                Id = user.Id,
                NumberCard = card.Number,
                PIN = pin
            };

            return response;
        }

        public async Task<GetUserDtoResponse> GetUser(string numberCard)
        {
            var user = await _repository.GetSet().Include(x => x.Cards).FirstOrDefaultAsync(x => x.Cards.Any(x => x.Number == numberCard));
            if(user == null)
            {
                throw new FileNotFoundException("El usuario no existe");
            }
            return _mapper.Map<GetUserDtoResponse>(user);
        }
    }
}
