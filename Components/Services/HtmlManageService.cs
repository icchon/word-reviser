using Aspose.Html;
using Aspose.Html.Collections;
using System.Diagnostics;
using Pandoc;
using SelectPdf;

namespace WordReviser.Components.Services
{
    public interface IHtmlManageService
    {
        public List<string> Read();
        public void AddCharacterConfig(string htmlPath);
        public void Html2Pdf(string htmlPath, string pdfPath);
        public Task Docx2HtmlAsync(string docxPath, string htmlPath);
        public List<string> Sentences { get;}
    }
    public class HtmlManageService:IHtmlManageService
    {
        private string _path = String.Empty;
        private readonly IDirectoryManageService _directoryManageService;

        private List<string> _sentences = new List<string>();
        public List<string> Sentences => Read();

        public HtmlManageService(IDirectoryManageService directoryManageService)
        {
            _directoryManageService = directoryManageService;
        }

        public List<string> Read()
        {
            _path = _directoryManageService.PreHtmlPath;

            if(_sentences.Count > 0)
            {
                return _sentences;
            }
            
            if (!File.Exists(_path))
            {
                Debug.WriteLine($"Failed to find HTML in {_path}");
                return _sentences;
            }

            var document = new HTMLDocument(_path);

            HTMLCollection paragraphs = document.GetElementsByTagName("p");
            foreach ( var paragraph in paragraphs )
            {
                _sentences.Add(paragraph.TextContent);
            }
            return _sentences;
        }

        public void AddCharacterConfig(string htmlPath)
        {
            using (var document = new HTMLDocument(htmlPath))
            {
                var head = document.QuerySelector("head");

                if (head != null)
                {
                    var meta = document.CreateElement("meta");
                    meta.SetAttribute("charset", "UTF-8");

                    head.InsertBefore(meta, head.FirstChild);
                }
                document.Save(htmlPath);
            }
        }

        public async Task Docx2HtmlAsync(string docxPath, string htmlPath)
        {
            try
            {
                await PandocInstance.Convert<DocxIn, HtmlOut>(docxPath, htmlPath);
                Debug.WriteLine($"Succeed to create Original HTML");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to create Original HTML, Error : {ex.Message}");
            }

            try
            {
                AddCharacterConfig(htmlPath);
                Debug.WriteLine("Succeed to add UTF-8");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to add UTF-8, Error : {ex.Message}");
            }
        }

        public void Html2Pdf(string htmlPath, string pdfPath)
        {
            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertUrl(htmlPath);
                doc.Save(pdfPath);
                doc.Close();

                Debug.WriteLine("Succeed to create PDF");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to create PDF, Error : {ex.Message}");
            }
        }
    }
}


