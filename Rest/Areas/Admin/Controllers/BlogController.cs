using Microsoft.AspNetCore.Mvc;
using Rest.Filters;

namespace Rest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyAuth("Admin")]
    public class BlogController : Controller
    {
        public IActionResult List()
        {
            ViewBag.ActiveNav = "Blog";
            return View();
        }
    }
}
