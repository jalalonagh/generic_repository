using Common.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FileExtensions
{
    public static class FileExtensions
    {
        private static IEnumerable<string> CodeFileExtensions = new string[] { ".cs", ".cshtml", ".css", ".js", ".json", ".sql", ".dart", ".html" };

        public static async Task<string> AutoSaveAsync(this IFormFile file, string path = "", string filename = "", string folder = "", string userId = "", string series = "")
        {
            if (file.Length > 0)
            {
                var todayFolder = DateTime.Now.ToPersianDateFolderName();
                var filePath = !string.IsNullOrEmpty(path) ? path : "";
                filePath = !string.IsNullOrEmpty(userId) ? Path.Combine(filePath, userId.Replace("-", "_")) : filePath;
                filePath = Path.Combine(filePath, todayFolder);
                filePath = !string.IsNullOrEmpty(folder) ? Path.Combine(filePath, folder) : filePath;
                filePath = !string.IsNullOrEmpty(series) ? Path.Combine(filePath, series) : filePath;
                filePath = !string.IsNullOrEmpty(filename) ? Path.Combine(filePath, filename) : Path.Combine(filePath, Path.GetTempFileName() + Path.GetExtension(file.FileName));

                CreateFolder(path, userId);
                CreateFolder(Path.Combine(path, userId.Replace("-", "_").Replace(" ", "_")), todayFolder);
                CreateFolder(Path.Combine(path, userId.Replace("-", "_").Replace(" ", "_"), todayFolder), folder);
                if (!string.IsNullOrEmpty(series))
                {
                    CreateFolder(Path.Combine(path, userId.Replace("-", "_").Replace(" ", "_"), todayFolder, folder), series);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    return filePath;
                }
            }
            return null;
        }

        public static bool IsCodeFile(this IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            return CodeFileExtensions.Where(w => w == ext).FirstOrDefault() != null ? true : false;
        }

        private static void CreateFolder(string basePath, string folder)
        {
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(Path.Combine(basePath, folder)))
            {
                Directory.CreateDirectory(Path.Combine(basePath, folder));
            }
        }

        public static string GetRandomName(this IFormFile file)
        {
            return DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
        }
    }
}
