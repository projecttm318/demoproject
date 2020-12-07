using NongSan.Models;
using System.Linq;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListUser()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Customers.ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

    }
}