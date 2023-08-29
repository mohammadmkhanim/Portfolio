using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Areas.Management.Models;
using Portfolio.Data.Entities;
using Portfolio.Data.UnitOfWork;
using Portfolio.Services;

namespace Portfolio.Areas.Management.Controllers
{
    [Area("Management")]
    public class AuthController : BaseController<AuthController>
    {

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(unitOfWork, mapper, configuration)
        {
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            string hashPassword = HashService.GenerateSha256Hash(loginViewModel.Password);
            string expectedPassword = _configuration.GetSection("AuthenticationSettings")["Password"];
            expectedPassword = HashService.GenerateSha256Hash(expectedPassword);

            if (hashPassword == expectedPassword)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1)
                };
                Response.Cookies.Append("Auth", hashPassword, cookieOptions);
            }
            else
            {
                ModelState.AddModelError("", "The password is wrong");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("Auth");
            return RedirectToAction("Index", "Home");
        }


    }
}