using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Rest.Data;
using Rest.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Filters
{
    public class MyAuth:Attribute,IAsyncAuthorizationFilter
    {
        private readonly string _role;
        public MyAuth(string role=null)
        {
            _role = role;
        }

       
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //var applicationDbContext = context.HttpContext
            //                                     .RequestServices
            //                                         .GetService<ApplicationDbContext>();


            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult(CookieAuthenticationDefaults.AuthenticationScheme);
                return;
            }

            if(_role != null)
            {
                var roleClaimValue = context.HttpContext
                                                .User
                                                    .Claims
                                                        .Where(c => c.Type == "role")
                                                            .FirstOrDefault()
                                                                .Value;


                var roleEnum = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), _role);

                if(Convert.ToInt32(roleClaimValue) != (int)roleEnum)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }

        }
    }
}
