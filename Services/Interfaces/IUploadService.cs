using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public interface IUploadService
    {
        Task<string> UploadImage(IFormFile file, string path);
    }
}