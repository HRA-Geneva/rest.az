using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rest.Areas.Admin.Models;
using Rest.Areas.Admin.Models.DTOs;
using Rest.Areas.Admin.Models.ViewModels;
using Rest.Data;
using Rest.Enums;
using Rest.Interfaces;
using Rest.Models;
using Rest.Models.DTOs;
using Rest.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public async Task<UserListVm> GetAllUsers(UserFilterModel model = null, int take = 10, int page = 1)
        {
            var users = await _context.Users
                .Include(c => c.UserRole)
                .Include(c => c.UserStatus)
                .Skip((page - 1) * take).Take(take).Select(c => new UserDto
                {
                    Name = c.Name,
                    Surname = c.Surname,
                    Image = c.Image,
                    Email = c.Email,
                    UserStatusId = c.UserStatusId,
                    UserName = c.UserName,
                    UserId = c.Id,
                    UserRoleId = c.UserRoleId,
                    UserRoleName = c.UserRole.Name,
                    UserStatusName = c.UserStatus.Name
                }).ToListAsync();


            var vm = new UserListVm();

            vm.Users = users;
            vm.TotalPage = Math.Ceiling((await _context.Users.CountAsync()) / (decimal)take);
            vm.CurrentPage = page;

            return vm;
        }

        public async Task<List<BlogDto>> GetUserBlogsById(int userId)
        {
            var userBlogs = await _context.UserBlogs
                .Include(c=>c.User)
                .Include(c=>c.Blog)
                .ThenInclude(c=>c.Category)
                .Where(c => c.UserId == userId).Select(c => new BlogDto
            {
                Image = c.Blog.Image,
                BlogId = c.Blog.Id,
                ShortDesc = c.Blog.ShortDesc,
                BlogStatusId = c.Blog.BlogStatusId,
                CategoryId = c.Blog.CategoryId,
                CategoryName = c.Blog.Category.Name,
                Description = c.Blog.Description,
                LikeCount = c.Blog.LikeCount,
                Published = (DateTime)c.Blog.Published,
                Title = c.Blog.Title,
                ViewCount = c.Blog.ViewCount,
                AuthorName = c.User.Name,
                AuthorSurname = c.User.Surname,
                AuthorImage = c.User.Image
            }).ToListAsync();

            return userBlogs;
        }

        public async Task<UserDto> GetUserInfoById(int userId)
        {
            var user = await _context.Users.Include(c => c.UserRole).Include(c => c.UserStatus).Where(c => c.Id == userId).Select(c => new UserDto
            {
                Name = c.Name,
                Surname = c.Surname,
                Image = c.Image,
                Email = c.Email,
                UserStatusId = c.UserStatusId,
                UserName = c.UserName,
                UserId = c.Id,
                UserRoleId = c.UserRoleId,
                UserRoleName = c.UserRole.Name,
                UserStatusName = c.UserStatus.Name
            }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> Login(ModelStateDictionary ModelState, LoginModel model, bool isAdminPanel = false)
        {

            //Validation

            if (!ModelState.IsValid)
                return false;

            //End validation

            User user = await _context.Users
                                    .Where(c => c.Email == model.Email)
                                        .FirstOrDefaultAsync();

            if (isAdminPanel == true && user.UserRoleId != (int)UserRoleEnum.Admin)
            {
                ModelState.AddModelError("", "Sizin sisteme giris huququnuz yoxdur.");
                return false;
            }

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
                     new Claim("role", user.UserRoleId.ToString()),
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
