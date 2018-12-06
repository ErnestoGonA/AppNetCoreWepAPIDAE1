using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppNetCoreWepAPIDAE1.Controllers
{

    public class IndexController : Controller
    {
        public IActionResult MetIndex()
        {
            return View();
        }
    }
}