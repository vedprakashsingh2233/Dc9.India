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
            if (Session["UserId"] == null)
            {
                return Redirect("~/UserLogin/Login");
            }
            else
            {
                return View();
            }
        }
    }
}