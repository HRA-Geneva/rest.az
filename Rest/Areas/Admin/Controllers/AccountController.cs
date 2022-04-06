using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Rest.Data;
using Rest.Interfaces;
using Rest.Models.ViewModels.Account;
using System.Threading.Tasks;

namespace Rest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _userService.Login(ModelState, model, true);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
