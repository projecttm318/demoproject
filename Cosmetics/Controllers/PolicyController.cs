using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        [Route("chinh-sach/{Slug}-{id}")]
        public ActionResult Detail(int id)
        {
            NongSanEntities db = new NongSanEntities();
            var item = db.Policies.FirstOrDefault(x => x.Id == id);
            return View(item);
        }
    }
}