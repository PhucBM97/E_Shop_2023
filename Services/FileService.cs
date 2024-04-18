using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _hostingEnvironment = webHostEnvironment;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Lấy đường dẫn wwwroot + tên thư mục (uploads)
                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Lấy tên file
                string fileName = Path.GetFileName(file.FileName);

                //Ghép đường dẫn Thư mục + tên file
                var filePath = Path.Combine(uploadsFolder, fileName);

                // file chưa tồn tại mới đc lưu
                if (!System.IO.File.Exists(filePath))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    // kết quả trả về "\\" -> "/"
                    var stringWithSingleSlash = filePath.Replace("\\", @"/");
                    int getIndex = stringWithSingleSlash.IndexOf("Uploads");
                    string urlPath = stringWithSingleSlash.Substring(getIndex - 1); // lấy dấu "/" trước
                    return urlPath;
                }
                else
                {
                    //file name đã tồn tại
                    return "";
                }
            }
            else
            {
                //k có file
                return "";
            }
        }
    }
}
