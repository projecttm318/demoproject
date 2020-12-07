using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        [Route("gioi-thieu")]
        public ActionResult Index()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Database.SqlQuery<About>("select * from About").ToList();
            return View(lst);
        }

        public ActionResult AboutHome()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Database.SqlQuery<About>("select * from About where Type=1").FirstOrDefault();
            return View(lst);
        }
    }
}