using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
