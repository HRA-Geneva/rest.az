using Microsoft.EntityFrameworkCore;
using Rest.Models;

namespace Rest.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {
         
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogStatus> BlogStatuses { get; set; }
        public virtual DbSet<BlogComment> BlogComments { get; set; }
        public virtual DbSet<BlogCommentStatus> BlogCommentStatuses { get; set; }
        public virtual DbSet<BlogLike> BlogLikes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<UserBlog> UserBlogs { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }

    }
}
