using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.models;
using System.IO;

namespace GraduationGuideline.web.Controllers
{
    public class PdfController : Controller
    {
        public PdfController()
        {
            
        }

        [HttpGet]
        [Route("/Pdf/GS8")]
        public IActionResult Gs8() {
            var file = new FileStream("./PDFs/GS8.pdf", FileMode.Open);
            return new FileStreamResult(file, "application/pdf");
        }
        [HttpGet]
        [Route("/Pdf/GS7")]
        public IActionResult Gs7() {
            var file = new FileStream("./PDFs/GS7.pdf", FileMode.Open);
            return new FileStreamResult(file, "application/pdf");
        }
    }
}
