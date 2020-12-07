using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            NongSanEntities db = new NongSanEntities();
            var page = db.Settings.FirstOrDefault();
            ViewBag.PageInfo = page;
            return View();
        }

        public ActionResult Footer()
        {
            NongSanEntities db = new NongSanEntities();
            var page = db.Settings.FirstOrDefault();
            ViewBag.PageInfo = page;
            return View();
        }

    }
}