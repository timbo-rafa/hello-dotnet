using Microsoft.AspNetCore.Mvc;
using OdeToFood2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood2.Controllers
{
    public class HomeController : Controller
    {
        //public string Index()
        public IActionResult Index()
        {
            var model = new Restaurant { Id = 1, Name = "Scott's Pizza Place" };
            return new ObjectResult(model);
            //return Content("Hello from the HomeController!");
        }
    }
}
