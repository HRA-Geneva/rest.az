using Microsoft.EntityFrameworkCore;
using Rest.Data;
using Rest.Enums;
using Rest.Interfaces;
using Rest.Models;
using Rest.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<BlogDto>> GetTrendingBlogs()
        {
           
            List<BlogDto> blogs = await _context.Blogs
                                 .Where(c =>
                                             c.BlogStatusId == (int)BlogStatusEnum.Active &&
                                             c.Published != null)

                                        .Include(c => c.Category)
                                        .Include(c => c.UserBlogs)
                                        .ThenInclude(c => c.User)
                                        .OrderByDescending(c => c.Published)
                                        .OrderByDescending(c => c.LikeCount)
                                        .OrderByDescending(c => c.BlogComments.Count)
                                            .Select(c => new BlogDto
                                            {
                                                Image = c.Image,
                                                BlogId = c.Id,
                                                ShortDesc = c.ShortDesc,
                                                BlogStatusId = c.BlogStatusId,
                                                CategoryId = c.CategoryId,
                                                CategoryName = c.Category.Name,
                                                Description = c.Description,
                                                LikeCount = c.LikeCount,
                                                Published = (DateTime)c.Published,
                                                Title = c.Title,
                                                ViewCount = c.ViewCount,
                                                AuthorName = c.UserBlogs.FirstOrDefault().User.Name,
                                                AuthorSurname = c.UserBlogs.FirstOrDefault().User.Surname,
                                                AuthorImage = c.UserBlogs.FirstOrDefault().User.Image
                                            })
                                            .Take(10)
                                            .ToListAsync();

            return blogs;
        }

        public async Task<List<CategoryBlogDto>> GetTrendingCategoryBlogs()
        {
            List<CategoryBlogDto> categoryBlogs = await _context.Categories
                                                                      .Include(c => c.Blogs)
                                                                      .ThenInclude(c => c.UserBlogs)
                                                                      .ThenInclude(c => c.User)
                                                                      .OrderByDescending(c => c.Blogs.Count)
                                                                     .Select(c => new CategoryBlogDto
                                                                     {
                                                                         CategoryId = c.Id,
                                                                         CategoryName = c.Name,
                                                                         Blogs = c.Blogs
                                                                         .OrderByDescending(a => a.BlogComments.Count)
                                                                         .OrderByDescending(a => a.LikeCount)
                                                                         .Select(a => new BlogDto
                                                                         {
                                                                             Image = a.Image,
                                                                             BlogId = a.Id,
                                                                             ShortDesc = a.ShortDesc,
                                                                             BlogStatusId = a.BlogStatusId,
                                                                             CategoryId = a.CategoryId,
                                                                             CategoryName = a.Category.Name,
                                                                             Description = a.Description,
                                                                             LikeCount = a.LikeCount,
                                                                             Published = (DateTime)a.Published,
                                                                             Title = a.Title,
                                                                             ViewCount = a.ViewCount,
                                                                             AuthorName = a.UserBlogs.FirstOrDefault().User.Name,
                                                                             AuthorSurname = a.UserBlogs.FirstOrDefault().User.Surname,
                                                                             AuthorImage = a.UserBlogs.FirstOrDefault().User.Image,
                                                                             PostCount = a.UserBlogs.Count
                                                                         })
                                                                         .ToList()
                                                                     }).Take(2).ToListAsync();

            categoryBlogs.ForEach(c => c.Blogs = c.Blogs.Take(3).ToList());

            return categoryBlogs;
        }
    }
}
