using AutoMapper;
using Challenge_ATM_API.Endpoints.BankEndpoints;
using Challenge_ATM_API.Endpoints.TransactionEndpoints;
using Challenge_ATM_API.Endpoints.UserEndpoints;
using Challenge_ATM_API.Entities;

namespace Challenge_ATM_API
{
    public class Mapper
    {
        public class BankProfile : Profile
        {
            public BankProfile()
            {
                CreateMap<CreateBankDtoRequest, Bank>();
                CreateMap<Bank, CreateBankDtoResponse>();
            }
        }

        public class UserProfile : Profile
        {
            public UserProfile()
            {
                CreateMap<User, GetUserDtoResponse>();
                CreateMap<CreateUserDtoRequest, User>();
            }
        }

        public class CardProfile : Profile
        {
            public CardProfile()
            {
                CreateMap<Card, GetBalanceDtoResponse>();
            }
        }
        public class TransactionsProfile : Profile
        {
            public TransactionsProfile()
            {
                CreateMap<Transaction, TransactionDtoResponse>();
            }
        }
    }
}
