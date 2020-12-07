using NongSan.Areas.Admin.DAO;
using NongSan.Models;
using NongSan.Models.DTO;
using NongSan.Models.Form;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Areas.Admin.DAO
{
    public class ProductDAO : BaseDAO
    {
        public List<ProductDTO> SearchList(BaseSearchForm filter)
        {
            var data = PreQuerySearchProduct(filter, "search");
            return (List<ProductDTO>)data;
        }
        public int CountTotal(BaseSearchForm filter)
        {
            var data = PreQuerySearchProduct(filter, "count");
            return (int)data;
        }
        public object PreQuerySearchProduct(BaseSearchForm filter, string mode)
        {
            string sql = "exec [sp_Search_Product_Condition]";
            sql = sql + string.Format("'{0}', ", mode);
            sql = sql + string.Format("'{0}', ", filter.StartRow);
            sql = sql + string.Format("'{0}', ", filter.Lenght);
            sql = sql + string.Format("'{0}', ", filter.SortCol);
            sql = sql + string.Format("'{0}', ", filter.SortType);
            sql = sql + string.Format("N" + "'{0}' ", filter.Info ?? "");
            if (mode == "search")
            {
                return Model.Database.SqlQuery<ProductDTO>(sql).ToList();
            }
            if (mode == "count")
            {
                return Model.Database.SqlQuery<int>(sql).ToList().FirstOrDefault();
            }
            return null;
        }

        public bool Delete(int id)
        {
            try
            {
                var del = Model.Products.FirstOrDefault(i => i.Id == id);
                if (del != null)
                {
                    Model.Products.Remove(del);
                    return Model.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}