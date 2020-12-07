using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NongSan.Models;
namespace NongSan.Areas.Admin.Controllers
{
    public class EmailRegisterController : BaseController
    {
        // GET: Admin/Contact

        NongSanEntities db = new NongSanEntities();
        public ActionResult Index()
        {
            var lst = db.Database.SqlQuery<EmailRegister>("select * from EmailRegister").ToList();
            return View(lst);
        }
    }
}