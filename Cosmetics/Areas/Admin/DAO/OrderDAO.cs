using NongSan.Areas.Admin.DTO;
using NongSan.Models;
using NongSan.Models.Form;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NongSan.Areas.Admin.DAO
{
    public class OrderDAO : BaseDAO
    {
        public List<OrderDTO> SearchList(OrderSearchForm filter)
        {
            var data = PreQuerySearchOrder(filter, "search");
            return (List<OrderDTO>)data;
        }
        public int CountTotal(OrderSearchForm filter)
        {
            var data = PreQuerySearchOrder(filter, "count");
            return (int)data;
        }
        public object PreQuerySearchOrder(OrderSearchForm filter, string mode)
        {
            string sql = "exec [sp_Search_Order_Condition]";
            sql = sql + string.Format("'{0}', ", mode);
            sql = sql + string.Format("'{0}', ", filter.StartRow);
            sql = sql + string.Format("'{0}', ", filter.Lenght);
            sql = sql + string.Format("'{0}', ", filter.SortCol);
            sql = sql + string.Format("'{0}', ", filter.SortType);
            sql = sql + string.Format("'{0}', ", filter.StartDate);
            sql = sql + string.Format("'{0}', ", filter.EndDate);
            sql = sql + string.Format("'{0}', ", filter.StatusID);
            sql = sql + string.Format("N" + "'{0}' ", filter.Info ?? "");
            if (mode == "search")
            {
                return Model.Database.SqlQuery<OrderDTO>(sql).ToList();
            }
            if (mode == "count")
            {
                return Model.Database.SqlQuery<int>(sql).ToList().FirstOrDefault();
            }
            return null;
        }
        public DataTable DataTableOrderShop(List<Order> lst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ngày", typeof(string));
            dt.Columns.Add("Họ Tên", typeof(string));
            dt.Columns.Add("SDT", typeof(string));
            dt.Columns.Add("Địa Chỉ", typeof(string));
            dt.Columns.Add("Tổng Tiền", typeof(string));

            foreach (var item in lst)
            {
                var lstDetail = Model.OrderDetails.Where(x => x.OrderID == item.ID);
                dt.Rows.Add(item.DateCreate.Value.ToString("dd/MM/yyy"), item.FullName, item.PhoneNumber, item.Address, item.TotalAmount.Value.ToString("#,##0"));
                foreach (var ob in lstDetail)
                {
                    dt.Rows.Add(ob.ProductName, ob.Price,ob.Quantity, item.TotalAmount.Value.ToString("#,##0"));
                }
            }
            return dt;
        }
    }
}