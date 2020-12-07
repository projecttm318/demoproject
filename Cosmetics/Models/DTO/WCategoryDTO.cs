using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.DTO
{
    public class WCategoryDTO:Category
    {
        public int ChildCount { get; set; }
    }
}