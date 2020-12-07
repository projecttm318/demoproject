using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.DTO
{
    public class CategoryDTO : Category
    {
        public int Level { get; set; }
    }
}