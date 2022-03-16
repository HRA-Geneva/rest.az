using Rest.Models.DTOs;
using System.Collections.Generic;

namespace Rest.Models.ViewModels.Home
{
    public class HomeVm
    {
        public List<BlogDto> Blogs { get; set; }
       
        public List<CategoryBlogDto> CategoryBlogs { get; set; }
        public List<PromotionDto> Promotions { get; set; }

        public HomeVm()
        {
            CategoryBlogs = new List<CategoryBlogDto>();
            Blogs = new List<BlogDto>();
        }
    }
}
