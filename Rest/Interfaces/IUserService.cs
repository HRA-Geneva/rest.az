using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Models.ViewModels.Account;
using System.Threading.Tasks;

namespace Rest.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(ModelStateDictionary ModelState, LoginModel model);
    }
}
