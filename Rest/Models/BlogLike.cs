namespace Rest.Models
{
    public class BlogLike:Entity
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }

        public virtual User User { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
