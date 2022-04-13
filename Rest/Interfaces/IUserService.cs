using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Areas.Admin.Models;
using Rest.Areas.Admin.Models.DTOs;
using Rest.Areas.Admin.Models.ViewModels;
using Rest.Models.DTOs;
using Rest.Models.ViewModels.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(ModelStateDictionary ModelState, LoginModel model,bool isAdminPanel=false);

        Task<UserListVm> GetAllUsers(UserFilterModel model=null,int take=10,int page=1);

        Task<List<BlogDto>> GetUserBlogsById(int userId);
        Task<UserDto> GetUserInfoById(int userId);
    }
}
