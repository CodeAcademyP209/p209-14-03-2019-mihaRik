using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReading.CustomFilterAttribute;

namespace BookReading.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class DashboardController : Controller
    {
        public DashboardController()
        {
            ViewBag.ActivePageDashboard = "active";
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}