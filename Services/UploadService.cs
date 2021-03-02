using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public UploadService(IWebHostEnvironment environment)
        {
            this._environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<string> UploadImage(IFormFile file, string folder)
        {
            string uploadFolder = "Uploads/" + folder;
            string fileName = this.GetUniqueFileName(file.FileName);
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), uploadFolder);
            var absoluteFilePath = Path.Combine(uploads, fileName);
            // Create dir if not exists
            System.IO.Directory.CreateDirectory(uploads);
            string relativeFilePath = Path.Combine(uploadFolder, fileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(absoluteFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return relativeFilePath;
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        }

        private bool CheckImageContentType(string fileContentType)
        {
            string[] availableContentTypes = { "image/gif", "image/jpeg", "image/png" };
            if (availableContentTypes.Contains(fileContentType))
                return true;

            return false;
        }
    }
}