using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Moradi.Controllers
{
    public class MoradiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
