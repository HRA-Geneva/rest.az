using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rest.Data;
using Rest.Enums;
using Rest.Interfaces;
using Rest.Models;
using Rest.Models.DTOs;
using Rest.Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IPromotionService _promotionService;
        public HomeController(IBlogService blogService,
                             IPromotionService promotionService)
        {
            _blogService = blogService;
            _promotionService = promotionService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVm vm = new HomeVm();

            vm.Blogs = await _blogService.GetTrendingBlogs();
            vm.CategoryBlogs = await _blogService.GetTrendingCategoryBlogs();
            vm.Promotions = await _promotionService.GetCurrentPromotions();

            return View(vm);
        }
    }
}
