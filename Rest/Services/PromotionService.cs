using Microsoft.EntityFrameworkCore;
using Rest.Data;
using Rest.Interfaces;
using Rest.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly ApplicationDbContext _context;
        public PromotionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PromotionDto>> GetCurrentPromotions()
        {
            List<PromotionDto> promotions = await _context.Promotions
                                                                .Where(c=> DateTime.Now >= c.StartDate && DateTime.Now <= c.EndDate)
                                                                .Include(c => c.User)
                                                                .Include(c => c.Blog)
                                                                .ThenInclude(c=>c.Category)
                                                                .Select(c => new PromotionDto
                                                                {
                                                                    BlogId = c.BlogId,
                                                                    ShortDesc = c.Blog.ShortDesc,
                                                                    AuthorSurname = c.User.Surname,
                                                                    BlogStatusId = c.Blog.BlogStatusId,
                                                                    AuthorImage = c.User.Image,
                                                                    AuthorName = c.User.Name,
                                                                    CategoryId = c.Blog.CategoryId,
                                                                    CategoryName = c.Blog.Category.Name,
                                                                    Description = c.Blog.Description,
                                                                    Image = c.Blog.Image,
                                                                    LikeCount = c.Blog.LikeCount,
                                                                    Published = (DateTime)c.Blog.Published,
                                                                    Title = c.Blog.Title,
                                                                    ViewCount = c.Blog.ViewCount
                                                                })
                                                                .ToListAsync();


            return promotions;
        }
    }
}
