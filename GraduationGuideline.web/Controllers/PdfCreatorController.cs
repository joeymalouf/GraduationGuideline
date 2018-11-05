using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.web.Utility;
using System.IO;
 
namespace GraduationGuideline.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {
        private IConverter _converter;
 
        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
        }
 
        [HttpGet]
        // [Route("api/[controller]/{form:int}")]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                DPI = 380
            };
 
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = GS8Generator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8" },
            };
 
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = new FileStream("./Controllers/GS8.pdf", FileMode.Open);
 
            return new FileStreamResult(file, "application/pdf");
        }
    }
}