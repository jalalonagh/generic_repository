using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Moradi.Models
{
    public class FileBox
    {
        public IFormFile file { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
    }
}
