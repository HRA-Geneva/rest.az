namespace Rest.Models
{
    public class BlogComment:Entity
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public int BlogCommentStatusId { get; set; }

        public virtual BlogCommentStatus BlogCommentStatus { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

    }
}
