using System.Collections.Generic;

namespace Rest.Models
{
    public class User:Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public string Image { get; set; }

        public int UserStatusId { get; set; }
        public virtual UserStatus UserStatus { get; set; }


        public int UserRoleId { get; set; }

        public virtual UserRole UserRole { get; set; }

        public virtual ICollection<UserBlog> UserBlogs { get; set; }
    }
}
