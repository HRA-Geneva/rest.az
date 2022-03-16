using System.Collections.Generic;

namespace Rest.Models.DTOs
{
    public class CategoryBlogDto
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<BlogDto> Blogs { get; set; }
        public CategoryBlogDto()
        {
            Blogs = new List<BlogDto>();
        }
    }
}
