using NongSan.Models;
using System.Linq;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        NongSanEntities db = new NongSanEntities();
        public ActionResult Index()
        {
            var lst = db.Database.SqlQuery<About>("select * from About").ToList();
            return View(lst);
        }
        public ActionResult AddEditAbout(int? id)
        {

            var item = db.Abouts.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                ViewData["Mode"] = "UPDATE";

            }
            else
            {
                item = new About();
                ViewData["Mode"] = "ADD";

            }

            return View(item);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddEditAbout(About item)
        {
            bool isAdd = Request["mode"] == "ADD";
            if (isAdd)
            {
                db.Abouts.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var old = db.Abouts.FirstOrDefault(f => f.Id == item.Id);
                UpdateModel(old);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(int id)
        {
            var item = db.Abouts.FirstOrDefault(x => x.Id == id);
            db.Abouts.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
