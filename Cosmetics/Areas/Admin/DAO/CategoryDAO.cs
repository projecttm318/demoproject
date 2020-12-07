using NongSan.Common;
using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NongSan.Areas.Admin.DAO
{
    public class CategoryDAO: BaseDAO
    {
        public Category GetById(int id)
        {
            return Model.Categories.FirstOrDefault(x => x.Id == id);
        }
        public List<Category> GetList()
        {
            return Model.Categories.ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                var del = Model.Categories.FirstOrDefault(i => i.Id == id);
                if (del != null)
                {
                    Model.Categories.Remove(del);
                    return Model.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddNew(Category item)
        {
            try
            {
                item.Slug = StringUtils.CreateUrl(item.CategoryName, "-");
                item.MetaTitle = item.CategoryName;
                item.ParrentID = 0;
                Model.Categories.Add(item);
                return Model.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Update(Category item)
        {
            try
            {
                Category update = Model.Categories.FirstOrDefault(x => x.Id == item.Id);
                if (update != null)
                {
                    update.Description = item.Description;
                    update.CategoryName = item.CategoryName;
                    update.MetaDescription = item.MetaDescription;
                    update.MetaKeyword = item.MetaKeyword;
                    update.Ord = item.Ord;
                    update.Slug = StringUtils.CreateUrl(item.CategoryName, "-");
                    update.MetaTitle = item.MetaTitle;
                    update.Url = item.Url;
                    update.ParrentID = 0;
                    update.IsHome = item.IsHome;
                    update.Avartar = item.Avartar;
                    return Model.SaveChanges() > 0;
                }
                return Model.SaveChanges() > 0;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;  // You can also choose to handle the exception here...
            }
        }
    }
}