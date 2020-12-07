using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.DTO
{
    public class OrderDetailDTO : Product
    {
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }
    }
}