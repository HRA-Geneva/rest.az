using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rest.Data;
using Rest.Interfaces;
using Rest.Models;
using Rest.Models.ViewModels.Account;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rest.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContext _httpContext;
        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<bool> Login(ModelStateDictionary ModelState, LoginModel model)
        {
            //Validation

            if (!ModelState.IsValid)
                return false;

            //End validation

            User user = await _context.Users
                                    .Where(c => c.Email == model.Email)
                                        .FirstOrDefaultAsync();

            if (user == null)
            {
                ModelState.AddModelError("", "Belə bir istifadəçi tapılmadı.");
                return false;
            }

            byte[] salt = user.Salt;

            byte[] hashed = KeyDerivation.Pbkdf2(
                 password: model.Password,
                 salt: salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 100000,
                 numBytesRequested: 256 / 8);


            if (user.Password.SequenceEqual(hashed))
            {
                var claims = new List<Claim>
                 {
                     new Claim("id",user.Id.ToString()),
                     new Claim("name", user.Name),
                     new Claim("surname", user.Surname),
                     new Claim("role", "Administrator"),
                 };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);


                await _httpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    IsPersistent = false
                });

                return true;
            }
            else
            {
                ModelState.AddModelError("", "Istifadəçi adı və ya şifrə yanlışdır.");
                return false;
            }

        }
    }
}
