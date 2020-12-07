using NongSan.Models;
using NongSan.Models.DAO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class NewController : BaseController
    {
        // GET: New
        public ActionResult Index()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Database.SqlQuery<New>("Select top 10 * from New Order by DatePost DESC").ToList();
            return View(lst);
        }
        [Route("tin-tuc/{Slug}-{id}")]
        public ActionResult Detail(int id)
        {
            NongSanEntities db = new NongSanEntities();
            var obj = db.Database.SqlQuery<New>(string.Format("Select * from New where Id={0}", id)).FirstOrDefault();
            var lstRelease = db.Database.SqlQuery<New>(string.Format("Select top 5 * from New  where Id <> {0} ORDER BY NEWID()", obj.Id)).ToList();
            ViewData["lstRelease"] = lstRelease;
            var lstCate = db.Categories.ToList();
            ViewData["Category"] = lstCate;
            return View(obj);
        }
        [Route("tin-tuc")]
        public ActionResult List(int? page)
        {
            NongSanEntities db = new NongSanEntities();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
           
            var lst = db.News.ToList().ToPagedList(pageNumber, pageSize);
          
            return View(lst);
        }
        //[Route("{Slug}-{id:int}")]
        //public ActionResult GetNewByCateID(int id, int? page)
        //{
        //    NongSanEntities db = new NongSanEntities();
        //    NewDAO dao = new NewDAO();
        //    int pageSize = 18;
        //    int pageNumber = (page ?? 1);
        //    var lst = dao.GetNewByCategoryID(id);
        //    var lstresult = lst.ToPagedList(pageNumber, pageSize);
        //    var cate = db.Category.FirstOrDefault(x => x.Id == id);
        //    ViewData["Category"] = cate;
        //    return View(lstresult);
        //}
    }
}