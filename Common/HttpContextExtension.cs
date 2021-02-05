using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    public static class HttpContextExtension
    {
        public static string GetUploadRootPath(this HttpContext value)
        {
            if (value != null)
            {
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\resources\\uploads");

                return rootPath;
            }

            return "";
        }
        public static string GetUploadWebPath(this HttpContext value)
        {
            if (value != null)
            {
                var webPath = Path.Combine(value.Request.Scheme + "://" + value.Request.Host, "/resources/uploads");

                return webPath;
            }

            return "";
        }
    }
}
