using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.CustomAuthorization;

namespace Portfolio.Areas.Management.Controllers
{
    [Area("Management")]
    [CustomAuthorization]
    public class HomeController : BaseController<HomeController>
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
