using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Portfolio.Services;

namespace Portfolio.CustomAuthorization
{
    public class CustomAuthorizationAttribute : ActionFilterAttribute
    {

        public CustomAuthorizationAttribute()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string password = context.HttpContext.Request.Cookies["Auth"];
            string expectedPassword = configuration.GetSection("AuthenticationSettings")["Password"];
            expectedPassword = HashService.GenerateSha256Hash(expectedPassword);

            if (password != expectedPassword)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}