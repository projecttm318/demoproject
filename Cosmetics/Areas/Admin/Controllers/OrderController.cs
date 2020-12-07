using NongSan.Areas.Admin.DAO;
using NongSan.Models;
using NongSan.Models.Form;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListOrder(OrderSearchForm condition)
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
            OrderDAO dao = new OrderDAO();
            var lst = dao.SearchList(condition);
            var sum = dao.CountTotal(condition);
            return Json(new { draw = draw, recordsTotal = sum, recordsFiltered = sum, data = lst });
        }
        public ActionResult ViewDetail(int id)
        {
            ViewData["ParentID"] = id;
            return View();
        }
        public JsonResult GetListOrderDetail(OrderDetailSearchForm condition)
        {
            int Start = int.Parse(Request["start"]);
            int Length = int.Parse(Request["length"]);
            int draw = int.Parse(Request["draw"]);
            condition.StartRow = Start;
            condition.Lenght = Length;

            //var ordercolumIndex = int.Parse(Request["order[0][column]"]);
            // string ordercolumName = Request[string.Format("columns[{0}][name]", ordercolumIndex)];
            // var orderasc = Request["order[0][dir]"].ToString();
            // dynamic orderby = Request["columns[" + ordercolumIndex + "][data]"].ToString();
            //condition.SortCol = ordercolumName;
            // condition.SortType = orderasc;
            OrderDetailDAO dao = new OrderDetailDAO();
            var lst = dao.SearchList(condition);
            var sum = dao.CountTotal(condition);
            return Json(new { draw = draw, recordsTotal = sum, recordsFiltered = sum, data = lst });
        }
        public JsonResult Delete(int id)
        {
            NongSanEntities db = new NongSanEntities();
            bool result = false;
            var item = db.Orders.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                db.Orders.Remove(item);
                result = db.SaveChanges() > 0;
            }
            if (result)
                return Json(new { Code = 1, message = "Xóa thành công" });
            return Json(new { Code = -1, message = "Xóa thành công" });

        }
        public JsonResult ExportExcel(OrderSearchForm condition)
        {

            OrderDAO dao = new OrderDAO();
            // DataTable data = dao.GetExcelTable(condition);
            string fileName = "DonHang_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss_ffff") + ".xlsx";
            string filePath = Server.MapPath("/Excel/" + fileName);
            condition.StartRow = 0;
            condition.Lenght = int.MaxValue;

            condition.SortCol = "DateCreate";
            condition.SortType = "desc";
            var lst = dao.SearchList(condition);
            ExcelPackage excel = new ExcelPackage(new System.IO.FileInfo(filePath));
            var ws = excel.Workbook.Worksheets.Add("Sheet1");
            int row = 1;
            ws.Cells["A" + row.ToString()].Value = "Ngày";
            ws.Cells["B" + row.ToString()].Value = "Họ Tên";
            ws.Cells["C" + row.ToString()].Value = "Số Điện Thoại";
            ws.Cells["D" + row.ToString()].Value = "Địa Chỉ";
            ws.Cells["E" + row.ToString()].Value = "Tổng Tiền";
            row++;
            foreach (var item in lst)
            {
                ws.Cells["A" + row.ToString()].Value = item.DateCreate.Value.ToString("dd/MM/yyy");
                ws.Cells["B" + row.ToString()].Value = item.FullName;
                ws.Cells["C" + row.ToString()].Value = item.PhoneNumber;
                ws.Cells["D" + row.ToString()].Value = item.Address;
                ws.Cells["E" + row.ToString()].Value = item.TotalAmount.Value.ToString("#,##0");
                row++;
                var lstDetail = dao.Model.OrderDetails.Where(x => x.OrderID == item.ID).ToList();
                foreach (var dt in lstDetail)
                {
                    ws.Cells["B" + row.ToString()].Value = dt.ProductName;
                    ws.Cells["C" + row.ToString()].Value = dt.Price.Value.ToString("#,##0");
                    ws.Cells["D" + row.ToString()].Value = dt.Quantity;
                    ws.Cells["E" + row.ToString()].Value = (dt.Quantity * dt.Price).Value.ToString("#,##0");
                }
                row++;
            }
            excel.Save();
            excel.Dispose();
            string url = "/" + fileName;
            return Json(new { Code = 1, Url = Url.Content(filePath) });

        }
        public ActionResult UpdateStatus(int id)
        {
            NongSanEntities db = new NongSanEntities();
            var od = db.Orders.FirstOrDefault(x => x.ID == id);
            ViewData["od"] = od;
            return View();
        }
        public JsonResult FnUpdateStatus(int id,int statusId)
        {
            NongSanEntities db = new NongSanEntities();
            var od = db.Orders.FirstOrDefault(x => x.ID == id);
            od.StatusID = statusId;
            db.SaveChanges();
            return Json(new { Code = 1 });
        }
    }
}