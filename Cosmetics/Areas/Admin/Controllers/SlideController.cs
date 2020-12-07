using CMS.Areas.Admin.DAO;
using NongSan.Common;
using NongSan.Models;
using System.Linq;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        SlideDAO dao = new SlideDAO();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListSlide()
        {
            var lst = dao.GetAll();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddEditSlide(Slide item, string mode)
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
                return Json(new { Code = 1, message = "Lưu Hình Ảnh Thành Công", data = item, mode = mode });
            return Json(new { Code = 0, message = "Lưu Hình Ảnh Thất Bại" });

        }
        public JsonResult Delete(int id)
        {
            if (dao.Delete(id))
            {
                return Json(new { Code = 1, message = "Xóa Hình Ảnh Thành Công!" });
            }
            return Json(new { Code = 0, message = "Xóa Hình Ảnh Thất Bại!" });
        }
    }
}
