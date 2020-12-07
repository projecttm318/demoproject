using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.DAO
{
    public class NewDAO
    {
        public List<Product> GetNewByCategoryID(int id)
        {
            NongSanEntities db = new NongSanEntities();
            List<Product> lst = new List<Product>();


            lst = db.Database.SqlQuery<Product>(string.Format("Select * from New n LEFT JOIN Menus menu on menu.Id= n.CategoryNewId where n.CategoryNewId={0}", id)).ToList();


            return lst;
        }
    }
}