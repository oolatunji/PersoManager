using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersoManager.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult RegisterCustomer()
        {
            return View();
        }

        public ActionResult ViewCustomer()
        {
            return View();
        }

        public ActionResult UpdateCustomer()
        {
            return View();
        }

        public ActionResult ViewCustomerCard()
        {
            return View();
        }

        public ActionResult DownloadCustomerPersoData()
        {
            return View();
        }

        public ActionResult ResetDownload()
        {
            return View();
        }
    }
}