using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Common
{
    public class Utility
    {
        public static string GetLoginUser()
        {
            var current = HttpContext.Current.Request.Cookies["USERNAME"];
            if (current == null)
                return "";
            return HttpContext.Current.Request.Cookies["USERNAME"].Value;
        }
    }
}