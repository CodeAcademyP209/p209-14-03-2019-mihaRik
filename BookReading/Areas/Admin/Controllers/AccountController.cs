using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookReading.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string pass)
        {
            if (username == "admin" && pass == "admin")
            {
                Session["admin"] = true;
            }
            else
            {
                ModelState.AddModelError("", "Username or password are incorrect.");
                return View();
            }

            return Redirect(HttpContext.Request["returnUrl"]??"/Admin/Dashboard/Index");
        }

        public ActionResult Logoff()
        {
            Session.Remove("admin");

            return RedirectToAction(nameof(Login));
        }
    }
}