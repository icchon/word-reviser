using System.Diagnostics;
using Microsoft.AspNetCore.Components.Forms;

namespace WordReviser.Components.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadAsync(IBrowserFile file);
    }
    public class FileUploadService: IFileUploadService
    {
        private IDirectoryManageService _directoryManageService;
        public FileUploadService(IDirectoryManageService directoryManageService)
        {
            _directoryManageService = directoryManageService;   
        }
        public async Task<string> UploadAsync(IBrowserFile file)
        {
            string path = Path.Combine(_directoryManageService.UploadDirectory, file.Name);

            using(FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    long size = file.Size;
                    Debug.WriteLine($"uploading file is {size/1000} KB");
                    await file.OpenReadStream(maxAllowedSize: Math.Max(size, 51200)).CopyToAsync(ms);
                    ms.WriteTo(fs);
                }
            }
            return path;
        }
    }
}
