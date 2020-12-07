using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Login(User user)
        {
            NongSanEntities db = new NongSanEntities();
            var use = db.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if(use==null)
            {
                return Json(new { Code = -1 });
            }
         
            var lst = db.Users.Where(u => u.UserName.ToLower() == user.UserName.ToLower() && u.PassWord == user.PassWord).FirstOrDefault();
            if (lst != null)
            {
                Session["UserNameAdmin"] = lst.UserName;
                Session["PassWordAdmin"] = lst.PassWord;
                Session.Timeout = 4800;
                return Json(new { Code = 1 });

            }
            else
            {
                  return Json(new { Code = 0 });

            }
        }
    }
}