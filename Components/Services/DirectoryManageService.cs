namespace WordReviser.Components.Services
{
    public interface IDirectoryManageService
    {
        public string RootDirectory { get; }
        public string UploadDirectory { get; }
        public string DownloadDirectory { get; }
        public string PreDocxPath { get; set; }
        public string PostDocxPath { get;}
        public string PreHtmlPath { get; }
        public string PostHtmlPath { get; }
        public string PrePdfPath { get; }
    }
    public class DirectoryManageService : IDirectoryManageService
    {
        private const string UPLOAD = "Upload";
        private const string DOWNLOAD = "Download";

        private IWebHostEnvironment _env;

        public string RootDirectory => _env.WebRootPath;
        public string UploadDirectory
        {
            get
            {
                string uploadDirectory = Path.Combine(RootDirectory, UPLOAD);
                if (!Path.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                return uploadDirectory;
            }
        }
        public string DownloadDirectory
        {
            get
            {
                string downloadDirectory = Path.Combine(RootDirectory, DOWNLOAD);
                if (!Path.Exists(downloadDirectory))
                {
                    Directory.CreateDirectory(downloadDirectory);
                }
                return downloadDirectory;
            }
        }
        public string PreDocxPath { get; set; } = String.Empty;
        public string PostDocxPath => File.Exists(PreDocxPath) ? Path.Combine(DownloadDirectory, $"{Path.GetFileNameWithoutExtension(PreDocxPath)}_revised.docx") : String.Empty;
        public string PreHtmlPath => File.Exists(PreDocxPath) ? Path.Combine(UploadDirectory, $"{Path.GetFileNameWithoutExtension(PreDocxPath)}.html") : String.Empty;
        public string PostHtmlPath => File.Exists(PreDocxPath) ? Path.Combine(DownloadDirectory, $"{Path.GetFileNameWithoutExtension(PreDocxPath)}_revised.html") : String.Empty;
        public string PrePdfPath => File.Exists(PreDocxPath) ? Path.Combine(UploadDirectory, $"{Path.GetFileNameWithoutExtension(PreDocxPath)}.pdf") : String.Empty;
        public DirectoryManageService(IWebHostEnvironment env)
        {
            _env = env;
        }
    }
}
