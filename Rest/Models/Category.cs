using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rest.Models
{
    public class Category:Entity
    {
        public string Name { get; set; }

        public int ParentId { get; set; }

        public virtual List<Blog> Blogs { get; set; }
    }
}
