using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dc9.India.Controllers
{
    public class VpshostingController : Controller
    {
        // GET: Vpshosting
        public ActionResult ManagedVps()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult checkout()
        {
            //if (Session["UserId"] == null)
            //{
            //    return Redirect("~/UserLogin/Login");
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }
        public ActionResult LinuxDedicatedSelfManagedSP()
        {
            return View();
        }
        public ActionResult WindowDedicatedSelfManagedSP()
        {
            return View();
        }
        public ActionResult LinuxVPSSelfManagedSP()
        {
            return View();
        }
        public ActionResult WindowVPSSelfManagedSP()
        {
            return View();
        }
    }
}