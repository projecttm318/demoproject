using System;
using System.Linq;
using System.Web.Mvc;
using NongSan.Models;
using NongSan.Models.Form;
using NongSan.Areas.Admin.DAO;

namespace NongSan.Areas.Admin.Controllers
{
    public class NewController : BaseController
    {
        NongSanEntities db = new NongSanEntities();
        //
        // GET: /Admin/Combo/
        public ActionResult Index()
        {

            return View();
        }
        public JsonResult GetListNew(BaseSearchForm condition)
        {
            int Start = int.Parse(Request["start"]);
            int Length = int.Parse(Request["length"]);
            int draw = int.Parse(Request["draw"]);
            condition.StartRow = Start;
            condition.Lenght = Length;

            var ordercolumIndex = int.Parse(Request["order[0][column]"]);
            string ordercolumName = Request[string.Format("columns[{0}][name]", ordercolumIndex)];
            var orderasc = Request["order[0][dir]"].ToString();
            dynamic orderby = Request["columns[" + ordercolumIndex + "][data]"].ToString();
            condition.SortCol = ordercolumName;
            condition.SortType = orderasc;
            NewDAO dao = new NewDAO();
            var lst = dao.SearchList(condition);
            var sum = dao.CountTotal(condition);
            return Json(new { draw = draw, recordsTotal = sum, recordsFiltered = sum, data = lst });
        }
        public ActionResult AddEditNew(int? id)
        {
            var item = db.News.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                ViewData["Mode"] = "UPDATE";

            }
            else
            {
                item = new New();
                ViewData["Mode"] = "ADD";

            }

            return View(item);
        }

        [ValidateInput(false)]
        [HttpPost]


        public ActionResult AddEditNew(New item)
        {
            bool isAdd = Request["mode"] == "ADD";
            if (isAdd)
            {
                item.Slug = Common.StringUtils.CreateUrl(item.Title, "-");
                item.DatePost = DateTime.Now;
                db.News.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var old = db.News.FirstOrDefault(f => f.Id == item.Id);
                UpdateModel(old);
                old.Slug = Common.StringUtils.CreateUrl(item.Title, "-");
                db.SaveChanges();
                return RedirectToAction("Index");

            }

        }
        public JsonResult Delete(int id)
        {
            bool result = false;
            var item = db.News.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                db.News.Remove(item);
                result = db.SaveChanges() > 0;
            }
            if (result)
                return Json(new { Code = 1, message="Xóa thành công" });
            return Json(new { Code = -1, message = "Xóa thành công" });

        }
    }
}