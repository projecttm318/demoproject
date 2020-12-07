using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Areas.Admin.DTO
{
    public class OrderDTO:Order
    {
        public string StatusName { get; set; }
    }
}