using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace NongSan.Common
{
    public static class OptimizationHelper
    {
        public static string ToBase64(this string imgPath)
        {
            var extension = Path.GetExtension(imgPath).Substring(1);
            using (Image image = Image.FromFile(imgPath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return "data:image/" + extension + ";base64," + base64String;
                }
            }
        }
    }
}