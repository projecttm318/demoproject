using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalItem { get; set; }
    }
}