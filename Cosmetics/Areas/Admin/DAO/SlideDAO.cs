using NongSan.Areas.Admin.DAO;
using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Areas.Admin.DAO
{
    public class SlideDAO : BaseDAO
    {
        public List<Slide> GetAll()
        {
            return Model.Slides.ToList();
        }
        public bool AddNew(Slide item)
        {
            try
            {

                Model.Slides.Add(item);
                return Model.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Update(Slide item)
        {
            try
            {
                Slide update = Model.Slides.FirstOrDefault(x => x.Id == item.Id);
                if (update != null)
                {
                  
                    update.Ord = item.Ord;
                    update.Title = item.Title;
                    update.Url = item.Url;
                    update.Image = item.Image;
                    return Model.SaveChanges() > 0;
                }
                return Model.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var del = Model.Slides.FirstOrDefault(i => i.Id == id);
                if (del != null)
                {
                    Model.Slides.Remove(del);
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