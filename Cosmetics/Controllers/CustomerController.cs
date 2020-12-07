using NongSan.Common;
using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var user = Utility.GetLoginUser();
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login");
            }
            NongSanEntities db = new NongSanEntities();
            var info = db.Customers.FirstOrDefault(x => x.UserName == user);
            return View(info);

        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public JsonResult SaveRegister(Customer item)
        {
            NongSanEntities db = new NongSanEntities();
            var old = db.Customers.FirstOrDefault(x => x.UserName == item.UserName);
            if (old != null)
            {
                return Json(new { Code = -1 });
            }
            db.Customers.Add(item);
            bool result = db.SaveChanges() > 0;

            if (result)
                return Json(new { Code = 1 });
            return Json(new { Code = 0 });


        }
        public JsonResult FnLogin(Customer item)
        {
            NongSanEntities db = new NongSanEntities();
            var old = db.Customers.FirstOrDefault(x => x.UserName == item.UserName);
            if (old == null)
            {
                return Json(new { Code = -1 });
            }
            else if (item.PassWord != old.PassWord)
            {
                return Json(new { Code = -2 });
            }
            var cart= ShoppingCart.Cart;
            string url = "";
            if(cart.Count>0)
            {
                url = "/Order/Payment";
            }
           else
            {
                url = "/Customer/Index";
            }

            //Đăng nhập ok
            SetCookie("USERNAME", item.UserName);
            return Json(new { Code = 1, url = url });

        }
        private void SetCookie(string CookieName, string value)
        {
            HttpCookie mycookie = new HttpCookie(CookieName, value);
            mycookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(mycookie);
        }
        public ActionResult ListOrder()
        {
            var user = Utility.GetLoginUser();
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login");
            }
            NongSanEntities db = new NongSanEntities();
            var lst = db.Orders.Where(x => x.CreateBy == user).ToList();
            return View(lst);

        }
        private void ExpireAllCookies()
        {

            int cookieCount = Request.Cookies.Count;
            for (var i = 0; i < cookieCount; i++)
            {
                var cookie = Request.Cookies[i];
                if (cookie != null)
                {
                    var cookieName = cookie.Name;


                    var expiredCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                    Response.Cookies.Add(expiredCookie);
                }
            }
            Request.Cookies.Clear();

        }
        public ActionResult LogOut()
        {
            ExpireAllCookies();
            return RedirectToAction("Index", "Home");
        }
    }
}