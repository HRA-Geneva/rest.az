using System;
using System.Collections.Generic;

namespace Rest.Models
{
    public class Blog:Entity
    {
        public string Title { get; set; }
        public string Image { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set;}

        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public DateTime? Published { get; set; }

        public int BlogStatusId { get; set; }
        public virtual BlogStatus BlogStatus { get; set; }

        public string IpAddress { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }


        public virtual ICollection<BlogComment> BlogComments { get; set; }
        public virtual ICollection<BlogLike> BlogLikes { get; set; }

        public virtual ICollection<UserBlog> UserBlogs { get; set; }
    }
}
