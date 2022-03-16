namespace Rest.Models
{
    public class UserBlog:Entity
    {
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
