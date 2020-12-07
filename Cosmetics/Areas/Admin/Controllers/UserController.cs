using NongSan.Models;
using System.Linq;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Admin/User/
        public ActionResult Login()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            NongSanEntities db = new NongSanEntities();
            var username = f["txtUserName"];
            var pass = f["txtPassWord"];
            var lst = db.Users.Where(u => u.UserName.ToLower() == username.ToLower() && u.PassWord == pass).FirstOrDefault();
            if (lst != null)
            {
                Session["UserNameAdmin"] = lst.UserName;
                Session["PassWordAdmin"] = lst.PassWord;
                Session.Timeout = 4800;
                return RedirectToAction("Index", "Product", new { Area = "Admin" });

            }
            else
            {
                return View();

            }
        }
        public JsonResult ChangePassword(string oldPass, string newPass, string confirmPass)
        {
            NongSanEntities db = new NongSanEntities();
            var user = Session["UserNameAdmin"].ToString();
            User us = db.Users.FirstOrDefault(i => i.UserName == user);
            if (us.PassWord != oldPass)
            {
                return Json(new { Code = -1, message = "Mật khẩu cũ không đúng." });
            }
            if (newPass != confirmPass)
            {
                return Json(new { Code = -1, message = "Mật khẩu mới không khớp" });
            }
            else
            {
                us.PassWord = newPass;
            }

            if (db.SaveChanges() > 0)
            {
                return Json(new { Code = 1, message = "Đổi mật khẩu thành công !" });
            }
            return Json(new { Code = -1, message = "Mật khẩu hoặc tên đăng nhập không đúng !" });

        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListUser()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Users.ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(User item, string mode)
        {
            NongSanEntities db = new NongSanEntities();
            bool result = false;
            if (mode == "ADD")
            {
                item.Sex = 1;
                item.Active = true;
                item.Admin = true;
                db.Users.Add(item);

            }
            else
            {
                User update = db.Users.FirstOrDefault(x => x.UserId == item.UserId);
                if (update != null)
                {
                    update.FullName = item.FullName;
                    update.UserName = item.UserName;
                    update.PassWord = item.PassWord;
                }
            }
            result = db.SaveChanges() > 0;
            if (result)
                return Json(new { Code = 1, message = "Lưu Thành Công" });
            return Json(new { Code = 0, message = "Lưu  Thất Bại" });

        }
        public JsonResult Delete(int id)
        {
            NongSanEntities db = new NongSanEntities();
            var old = db.Users.FirstOrDefault(x => x.UserId == id);
            db.Users.Remove(old);
            if (db.SaveChanges()>0)
            {
                return Json(new { Code = 1, message = "Xóa User Thành Công!" });
            }
            return Json(new { Code = 0, message = "Xóa User Thất Bại!" });
        }
    }
}