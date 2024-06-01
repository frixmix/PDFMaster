using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YourApp.Services;

namespace PDFMaster.Controllers
{
    public class PdfController : Controller
    {
        private readonly PdfMerger _pdfMerger;

        public PdfController(PdfMerger pdfMerger)
        {
            _pdfMerger = pdfMerger;

        }

        [HttpGet]
        public IActionResult MergePdfs()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ConvertDocxToPdf()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MergePdfs(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ModelState.AddModelError("", "Please upload at least one PDF file.");
                return View();
            }

            var tempFiles = new List<string>();
            try
            {
                foreach (var file in files)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    tempFiles.Add(filePath);
                }

                var outputFilePath = Path.Combine(Path.GetTempPath(), "merged.pdf");
                _pdfMerger.MergePdfs(outputFilePath, tempFiles.ToArray());

                // Automatyczne pobieranie pliku po złączeniu
                var outputStream = new FileStream(outputFilePath, FileMode.Open, FileAccess.Read);
                return File(outputStream, "application/pdf", "merged.pdf");
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog or NLog)
                Console.WriteLine($"An error occurred: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while merging the PDFs. Please try again.");
                return View();
            }
            finally
            {
                // Clean up temporary files
                foreach (var tempFile in tempFiles)
                {
                    if (System.IO.File.Exists(tempFile))
                    {
                        System.IO.File.Delete(tempFile);
                    }
                }
            }
        }
        

    }
}
