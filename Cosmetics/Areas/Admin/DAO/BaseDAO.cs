using NongSan.Models;

namespace NongSan.Areas.Admin.DAO
{
    public class BaseDAO
    {
        public NongSanEntities Model { get; set; }
        public BaseDAO()
        {
            Model = new NongSanEntities();
        }
    }
}