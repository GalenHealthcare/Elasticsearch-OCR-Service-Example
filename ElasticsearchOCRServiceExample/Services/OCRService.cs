using Tesseract;

namespace ElasticsearchOCRServiceExample.Services
{
    /**
     * This implementation of the OCRService requires Tesseract to
     * be installed locally, but different implementations can be swapped
     * in using other services (i.e. Azure's Computer Vision API)
     */
    public class OCRService
    {
        private TesseractEngine _engine { get; set; }

        public OCRService()
        {
            _engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
        }

        public string GetTextFromImage(byte[] imageData)
        {
            using Pix img = Pix.LoadFromMemory(imageData);
            using Page page = _engine.Process(img);

            return page.GetText();
        }
    }
}
