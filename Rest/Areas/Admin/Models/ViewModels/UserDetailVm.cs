using Rest.Areas.Admin.Models.DTOs;
using Rest.Models.DTOs;
using System.Collections.Generic;

namespace Rest.Areas.Admin.Models.ViewModels
{
    public class UserDetailVm
    {
        public List<BlogDto> Blogs { get; set; }
        public UserDto Profile { get; set; }
    }
}
