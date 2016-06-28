﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersoManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Login";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Title = "Dashboard";

            return View();
        }
    }
}
