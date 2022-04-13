using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rest.Areas.Admin.Models.ViewModels;
using Rest.Filters;
using Rest.Interfaces;
using System;
using System.Threading.Tasks;

namespace Rest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyAuth]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<IActionResult> List(int page=1)
        {
            ViewBag.ActiveNav = "User";

            int take = Convert.ToInt32(_configuration["Lists:User"]);
            var result = await _userService.GetAllUsers(take: take,page:page);
            return View(result);
        }

        public async Task<IActionResult> Detail(int userId)
        {
            var userInfo = await _userService.GetUserInfoById(userId);
            var userBlogs = await _userService.GetUserBlogsById(userId);

            var vm = new UserDetailVm();

            vm.Blogs = userBlogs;
            vm.Profile = userInfo;

            return View(vm);
        }
    }
}
