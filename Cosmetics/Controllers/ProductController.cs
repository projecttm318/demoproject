using NongSan.Models;
using NongSan.Models.DAO;
using NongSan.Models.DTO;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [Route("san-pham/{Slug}-{id}")]
        public ActionResult Detail(int id)
        {
            NongSanEntities db = new NongSanEntities();
            var ob = db.Database.SqlQuery<Product>(string.Format("select * from Product where Id={0}", id)).FirstOrDefault();
            var lstCate = db.Categories.ToList();
            
            ViewData["lstCate"] = lstCate;
            return View(ob);
        }
        public ActionResult IsHome()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Database.SqlQuery<ProductDTO>("select top 3 id,Slug,ProductName,Price,Image from Product order by DateCreate desc").ToList();
            return View(lst);
        }
        [Route("{Slug}-{id:int}")]
        public ActionResult GetProductByCateID(int id, int? page)
        {
            NongSanEntities db = new NongSanEntities();
            ProductDAO dao = new ProductDAO();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var lst = dao.GetProductByCategoryID(id, "LIST");
            var lstresult = lst.ToPagedList(pageNumber, pageSize);
            var cate = db.Categories.FirstOrDefault(x => x.Id == id);
            var lstCate = db.Categories.ToList();
            ViewData["Category"] = cate;
            ViewData["lstCate"] = lstCate;
            return View(lstresult);
        }

        public ActionResult List()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Products.Take(9).OrderByDescending(x=>x.DateCreate).ToList();
            var lstCate = db.Categories.ToList();
            ViewData["lst"] = lst;
            ViewData["lstCate"] = lstCate;
            return View();
        }


    }
}