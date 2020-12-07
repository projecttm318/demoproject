using NongSan.Models;
using System.Linq;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class PageInfoController : BaseController
    {
        NongSanEntities db = new NongSanEntities();

        public ActionResult Index()
        {

            return View();
        }
        public JsonResult GetListSetting()
        {
            var lst = db.Settings.ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddEditSetting(int? id)
        {

            var item = db.Settings.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                ViewData["Mode"] = "UPDATE";

            }
            else
            {
                item = new Setting();
                ViewData["Mode"] = "ADD";

            }

            return View(item);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(Setting item)
        {
            bool result = false;
            var old = db.Settings.FirstOrDefault(f => f.ID == item.ID);
            if (old != null)
            {
                //old.Address = item.Address;
                //old.Email = item.Email;
                //old.Favicon = item.Favicon;
                //old.Logo = item.Logo;
                //old.MetaDescription = item.MetaDescription;
                //old.MetaKeyWord = item.MetaKeyWord;
                //old.MetaTitle = item.MetaTitle;
                //old.PhoneNumber = item.PhoneNumber;
                //old.Sologan = item.Sologan;
                //old.Meta1 = item.Meta1;
                //old.PixelFacebook = item.PixelFacebook;
                this.UpdateModel(old);
            }
            result = db.SaveChanges() > 0;
            if (result)
                return Json(new { Code = 1, message = "Lưu Thành Công" });
            return Json(new { Code = 0, message = "Lưu Thất Bại" });
        }
    }
}