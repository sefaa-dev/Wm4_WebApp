using ItServiceApp.InjectOrnek;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyDependency _myDependency;
        public HomeController(IMyDependency myDependency)
        {
            _myDependency = myDependency;
        }
        
        public IActionResult Index()
        {

            _myDependency.Log("Home/Index'e girildi");
            return View();
        }
    }
}
