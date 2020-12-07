using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Slug  { get; set; }

    
}
}