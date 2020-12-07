using NongSan.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}