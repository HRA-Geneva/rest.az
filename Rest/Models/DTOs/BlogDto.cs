using System;

namespace Rest.Models.DTOs
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        public int BlogStatusId { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }

        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorImage { get; set; }
        public int PostCount { get; set; }

    }
}
