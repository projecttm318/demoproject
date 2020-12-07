using System.Web.Mvc;
using System.Web.Routing;

namespace NongSan.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["UserNameAdmin"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index",
                    
                }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}