using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class SlideController : Controller
    {
        // GET: Slide
        public ActionResult Index()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Slides.ToList();
            return View(lst);
        }
    }
}