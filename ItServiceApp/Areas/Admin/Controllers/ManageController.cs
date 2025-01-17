﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;   

namespace ItServiceApp.Areas.Admin.Controllers       
{
    public class ManageController :  AdminBaseController
    {
       public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Subscriptions()
        {
            return View();
        }
    }
}
