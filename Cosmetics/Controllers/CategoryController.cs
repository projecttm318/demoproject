using NongSan.Models;
using NongSan.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        public ActionResult Index()
        {
            NongSanEntities db = new NongSanEntities();

            var lstCate = db.Categories.ToList();
            return View(lstCate);
        }
    }
}