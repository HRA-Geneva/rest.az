using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rest.Data;
using Rest.Models;
using Rest.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewBag.Title = "Login";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //Validation


            //End validation

            User user = await _context.Users
                                    .Where(c => c.Email == model.Email)
                                        .FirstOrDefaultAsync();

            if (user == null)
            {
                //todo
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


                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    IsPersistent = false
                });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //todo error
            }

            return View();
        }
    }
}
