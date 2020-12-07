using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.DAO
{
    public class ProductDAO
    {
        public List<Product> GetProductByCategoryID(int id, string mode)
        {
            NongSanEntities db = new NongSanEntities();
            List<Product> lst = new List<Product>();
            if (mode == "Home")
            {
                lst = db.Database.SqlQuery<Product>(string.Format("select  p.* from Product p LEFT JOIN Category cate on cate.Id= p.CategoryId where p.CategoryId={0}", id)).ToList();
            }
            else
            {
                lst = db.Database.SqlQuery<Product>(string.Format("Select * from Product p LEFT JOIN Category cate on cate.Id= p.CategoryId where p.CategoryId={0}", id)).ToList();
            }

            return lst;
        }
        public List<Category> GetCateList()
        {
            NongSanEntities db = new NongSanEntities();
            return db.Categories.ToList();
        }
        public List<Product> GetProductIsHot()
        {
            NongSanEntities db = new NongSanEntities();
            var lst = db.Database.SqlQuery<Product>("select top 6 * from Product where IsHome=1").ToList();
            return lst;
        }
    }
}