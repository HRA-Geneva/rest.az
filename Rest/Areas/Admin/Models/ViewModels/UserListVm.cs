using Rest.Areas.Admin.Models.DTOs;
using System.Collections.Generic;

namespace Rest.Areas.Admin.Models.ViewModels
{
    public class UserListVm
    {
        public List<UserDto> Users { get; set; }
        public decimal TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
