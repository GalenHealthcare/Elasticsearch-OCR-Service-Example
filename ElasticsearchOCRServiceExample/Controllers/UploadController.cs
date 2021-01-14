using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Net;
using Tesseract;
using System.IO;
using ElasticsearchOCRServiceExample.Services;

namespace ElasticsearchOCRServiceExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private ElasticsearchService _elasticsearchService { get; set; }
        private OCRService _ocrService { get; set; }

        public UploadController(ElasticsearchService elasticsearchService, OCRService ocrService)
        {
            _elasticsearchService = elasticsearchService;
            _ocrService = ocrService;
        }

        [HttpPost]
        public async Task<ActionResult> UploadDocument(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using MemoryStream ms = new MemoryStream();

                await file.CopyToAsync(ms);

                byte[] imageData = ms.ToArray();

                string fileTextContent = _ocrService.GetTextFromImage(imageData);

                await _elasticsearchService.IndexScannedDocument(file.FileName, fileTextContent);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
