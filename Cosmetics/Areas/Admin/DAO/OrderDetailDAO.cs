using NongSan.Models;
using NongSan.Models.DTO;
using NongSan.Models.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Areas.Admin.DAO
{
    public class OrderDetailDAO : BaseDAO
    {
        public List<OrderDetailDTO> SearchList(OrderDetailSearchForm filter)
        {
            var data = PreQuerySearchOrderDetail(filter, "search");
            return (List<OrderDetailDTO>)data;
        }
        public int CountTotal(OrderDetailSearchForm filter)
        {
            var data = PreQuerySearchOrderDetail(filter, "count");
            return (int)data;
        }
        public object PreQuerySearchOrderDetail(OrderDetailSearchForm filter, string mode)
        {
            string sql = "exec [sp_Search_OrderDetail_Condition]";
            sql = sql + string.Format("'{0}', ", mode);
            sql = sql + string.Format("'{0}', ", filter.StartRow);
            sql = sql + string.Format("'{0}', ", filter.Lenght);
            sql = sql + string.Format("'{0}', ", filter.SortCol);
            sql = sql + string.Format("'{0}', ", filter.SortType);
            sql = sql + string.Format("'{0}' ", filter.ParentID);
            if (mode == "search")
            {
                return Model.Database.SqlQuery<OrderDetailDTO>(sql).ToList();
            }
            if (mode == "count")
            {
                return Model.Database.SqlQuery<int>(sql).ToList().FirstOrDefault();
            }
            return null;
        }

    }
}