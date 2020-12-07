using System.Web.Mvc;
using NongSan.Models;
using NongSan.Areas.Admin.DAO;
using NongSan.Areas.Admin.Controllers;
using NongSan.Common;

namespace NongSan.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryDAO dao = new CategoryDAO();
        //
        // GET: /Admin/CateProduct/
        public ActionResult Index()
        {

            return View();
        }
        public JsonResult GetListCategory()
        {
            var lst = dao.GetList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult Delete(int id)
        {
            if (dao.Delete(id))
            {
                return Json(new { Code = 1, message = "Xóa Danh Mục Thành Công!" });
            }
            return Json(new { Code = 0, message = "Xóa Danh Mục Thất Bại!" });
        }
        public ActionResult Update(int id)
        {
            string mode = "ADD";
            ViewData["OrderID"] = id;
            Category ac;
            ac = dao.GetById(id);
            if (ac == null)
            {
                mode = "ADD";
                ac = new Category();
                ViewData["Title"] = "Thêm Mới Tài Khoản";
            }
            else
            {
                mode = "EDIT";
                ViewData["Title"] = "Cập Nhật Tài Khoản";
            }
            ViewData["Mode"] = mode;
            ViewData["obj"] = ac;
            return View();
        }
        [HttpPost]
        public JsonResult AddNewCate(Category item, string mode)
        {
            bool result = false;
            if (mode == "ADD")
            {
                result = dao.AddNew(item);
            }
            else
            {
                result = dao.Update(item);
            }
            if (result)
                return Json(new { Code = 1, message = "Lưu Danh Mục Thành Công", data = item, mode = mode });
            return Json(new { Code = 0, message = "Lưu Danh Mục Thất Bại" });

        }
    }
}