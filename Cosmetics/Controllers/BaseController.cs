using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class BaseController : Controller
    {
        NongSanEntities db = new NongSanEntities();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.PageInfo = db.Settings.FirstOrDefault();
            base.OnActionExecuting(filterContext);
        }
    }
}