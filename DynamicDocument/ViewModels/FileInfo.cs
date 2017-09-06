using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDocument.ViewModels
{
    public class FileInfo
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public FileInfo(string filePath)
        {
            if (File.Exists(filePath))
            {
                FilePath = filePath;
                FileName = Path.GetFileName(filePath);
                FileExtension = Path.GetExtension(filePath);
            }
        }
    }
}
