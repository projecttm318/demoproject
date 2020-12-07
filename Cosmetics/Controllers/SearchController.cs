using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string query)
        {
          NongSanEntities db = new NongSanEntities();
            var lst = db.Database.SqlQuery<Product>(string.Format("exec Sp_Search_Info N'{0}'", query)).ToList();
            return View(lst);
        }
    }
}