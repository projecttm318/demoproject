using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Models.Form
{
    public class OrderSearchForm : BaseForm
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StatusID { get; set; }
    }
}