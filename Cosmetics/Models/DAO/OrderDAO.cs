using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.DAO
{
    public class OrderDAO
    {
        public string GetSearchText(Order od)
        {
            string text = od.FullName + od.PhoneNumber + od.Address;
            return Common.StringUtils.RemoveSign4VietnameseString(text).ToLower();
        }
        public string GetNextID()
        {
            NongSan.Models.NongSanEntities db = new NongSanEntities();
            string sql = "exec [Sp_GetODID]";
            string id = db.Database.SqlQuery<string>(sql).FirstOrDefault();
            return id;
        }
    }
}