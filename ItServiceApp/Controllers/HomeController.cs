using ItServiceApp.Data;
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
        private readonly MyContext _dbContext;
        public HomeController(IMyDependency myDependency, MyContext dbContext)
        {
            _myDependency = myDependency;
            _dbContext = dbContext;
        }
        
        public IActionResult Index()
        {

            _myDependency.Log("Home/Index'e girildi");
            var data = _dbContext.SubscriptionTypes.ToList();
            return View();
        }
    }
}
