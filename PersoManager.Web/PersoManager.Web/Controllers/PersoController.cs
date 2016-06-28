using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersoManager.Web.Controllers
{
    public class PersoController : Controller
    {
        // GET: Perso
        public ActionResult DownloadPersoFile()
        {
            return View();
        }

        public ActionResult ResetDownload()
        {
            return View();
        }
    }
}