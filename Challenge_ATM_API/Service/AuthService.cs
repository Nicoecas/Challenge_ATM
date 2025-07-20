using Challenge_ATM_API.Data;
using Challenge_ATM_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Challenge_ATM_API.Service
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(Card card, string pin);
    }
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;

        public AuthService(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<string?> LoginAsync(Card card, string pin)
        {
            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null!, card.PIN, pin);

            if (result == PasswordVerificationResult.Success)
            {
                return _jwtService.GenerateToken(card.Number);
            }

            return null;
        }

        public static string HashearPIN(string pin)
        {
            var hasher = new PasswordHasher<object>();
            return hasher.HashPassword(null!, pin);
        }
    }
}