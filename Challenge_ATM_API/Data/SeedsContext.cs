using Challenge_ATM_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Challenge_ATM_API.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedsContext(this ModelBuilder modelBuilder)
        {
            var fecha = new DateTime(2025, 7, 19);
            var fechaVenc = new DateTime(2030, 7, 19);
            var pinHasheado = new PasswordHasher<object>().HashPassword(null!, "1234");

            modelBuilder.Entity<Bank>().HasData(new Bank
            {
                Id = 1,
                Name = "BANK_ATM",
                Country = "Argentina",
                City = "Buenos Aires",
                Address = "Siempre viva 123",
                CreatedDate = fecha,
                UpdatedDate = fecha
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Juan",
                Surname = "Perez",
                Email = "JuanPerez@email.com",
                DNI = "11222333",
                CUIT = "00-11222333-4",
                BankId = 1,
                CreatedDate = fecha,
                UpdatedDate = fecha
            });

            modelBuilder.Entity<Card>().HasData(new Card
            {
                Id = 1,
                UserId = 1,
                Number = "1111111111111111",
                IsLocked = false,
                PIN = pinHasheado,
                Balance = 0,
                ExpirationDate = fechaVenc,
                CreatedDate = fecha,
                UpdatedDate = fecha
            });
        }
    }
}
