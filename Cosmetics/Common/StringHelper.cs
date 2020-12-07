using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSan.Common
{
    public static class StringHelper
    {
        public static string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length);
            }
            return source;
        }
    }
}