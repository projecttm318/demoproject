using NongSan.Models;
using NongSan.Models.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Areas.Admin.DAO
{
    public class NewDAO : BaseDAO
    {
        public List<New> SearchList(BaseSearchForm filter)
        {
            var data = PreQuerySearchNew(filter, "search");
            return (List<New>)data;
        }
        public int CountTotal(BaseSearchForm filter)
        {
            var data = PreQuerySearchNew(filter, "count");
            return (int)data;
        }
        public object PreQuerySearchNew(BaseSearchForm filter, string mode)
        {
            string sql = "exec [sp_Search_New_Condition]";
            sql = sql + string.Format("'{0}', ", mode);
            sql = sql + string.Format("'{0}', ", filter.StartRow);
            sql = sql + string.Format("'{0}', ", filter.Lenght);
            sql = sql + string.Format("'{0}', ", filter.SortCol);
            sql = sql + string.Format("'{0}', ", filter.SortType);
            sql = sql + string.Format("N" + "'{0}' ", filter.Info ?? "");
            if (mode == "search")
            {
                return Model.Database.SqlQuery<New>(sql).ToList();
            }
            if (mode == "count")
            {
                return Model.Database.SqlQuery<int>(sql).ToList().FirstOrDefault();
            }
            return null;
        }
    }
}