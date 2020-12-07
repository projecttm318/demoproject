using CMS.Areas.Admin.DAO;
using NongSan.Common;
using NongSan.Models;
using NongSan.Models.Form;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ProductDAO dao = new ProductDAO();
        NongSanEntities db = new NongSanEntities();
        //
        // GET: /Admin/Product/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListProduct(BaseSearchForm condition)
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
            ProductDAO dao = new ProductDAO();
            var lst = dao.SearchList(condition);
            var sum = dao.CountTotal(condition);


            //var cloneToDebug = lst.Select(x => new Product
            //{
            //     ProductName = x.ProductName,
            //     Price = x.Price,
            //     Images=x.Images,

            //});

            return Json(new { draw = draw, recordsTotal = sum, recordsFiltered = sum, data = lst });
        }

        public ActionResult AddEditProduct(int? id)
        {
            var item = db.Products.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                ViewData["Mode"] = "UPDATE";
            }
            else
            {
                item = new Product();
                ViewData["Mode"] = "ADD";

            }

            return View(item);
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult SaveProduct(Product item, string mode)
        {
            bool result = false;

            if (mode == "ADD")
            {
                item.Slug = Common.StringUtils.CreateUrl(item.ProductName, "-");
                item.DateCreate = DateTime.Now;
                db.Products.Add(item);
                result = db.SaveChanges() > 0;
            }
            else
            {
                var old = db.Products.FirstOrDefault(f => f.Id == item.Id);
                old.Slug = Common.StringUtils.CreateUrl(item.ProductName, "-");
                old.ProductName = item.ProductName;
                old.Content = item.Content;
                old.DateCreate = item.DateCreate;
                old.Price = item.Price;
                old.Type = item.Type;
                old.MetaDescription = item.MetaDescription;
                old.MetaKeyWord = item.MetaKeyWord;
                old.MetaTitle = item.MetaTitle;
                old.CategoryId = item.CategoryId;
                old.Slug = Common.StringUtils.CreateUrl(item.ProductName, "-");
                old.Image = item.Image;
                old.IsHome = item.IsHome;
                old.Images1 = item.Images1;
                old.Images2 = item.Images2;
                old.Images3 = item.Images3;
                old.Images4 = item.Images4;
                old.Images5 = item.Images5;
                old.Images6 = item.Images6;
                old.Promotion1 = item.Promotion1;
                old.Promotion2 = item.Promotion2;
                old.Promotion3 = item.Promotion3;
                old.Promotion4 = item.Promotion4;
                old.Promotion5 = item.Promotion5;
                old.Promotion6 = item.Promotion6;
                old.Promotion7 = item.Promotion7;
                old.PixelFacebook = item.PixelFacebook;


                result = db.SaveChanges() > 0;
            }
            if (result)
                return Json(new { Code = 1, data = item, mode = mode, message = "Lưu sản phẩm thành công" });
            return Json(new { Code = -1, message = "Lưu sản phẩm thất bại" });

        }


        public JsonResult Delete(int id)
        {
            var item = db.Products.FirstOrDefault(x => x.Id == id);
            bool result = false;
            if (item != null)
            {
                db.Products.Remove(item);
                result = db.SaveChanges() > 0;
            }
            if (result)
                return Json(new { Code = 1, message = "Xóa thành công" });
            return Json(new { Code = -1, message = "Xóa thất bại" });
        }
        public JsonResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                string path = "/Uploads/Images/";
                string newFileName = DateTime.Now.ToString("ddMMyyyyhhmmsstt") + Request.Files[0].FileName;
                Request.Files[0].SaveAs(Server.MapPath("/Uploads/Images" + "\\" + newFileName));
                string FilePath = path + newFileName;
                return Json(new { Code = 1, data = FilePath, JsonRequestBehavior.AllowGet });
            }
            return Json(new { Code = -1 }, JsonRequestBehavior.AllowGet);
        }

    }
}