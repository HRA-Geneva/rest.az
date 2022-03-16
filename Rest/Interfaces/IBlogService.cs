using Rest.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogDto>> GetTrendingBlogs();
        Task<List<CategoryBlogDto>> GetTrendingCategoryBlogs();
    }
}
