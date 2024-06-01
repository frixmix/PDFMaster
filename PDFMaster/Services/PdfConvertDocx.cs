//using GrapeCity.Documents.Word;
//using GrapeCity.Documents.Pdf;
//using GrapeCity.Documents.Word.Layout;
//using System.IO;

//namespace PDFMaster.Services
//{
//    public class PdfConvertDocx
//    {
//        public byte[] ConvertDocxToPdf(string docxFilePath)
//        {
//            // Load the DOCX file into a GcWordDocument.
//            var wordDocument = new GcWordDocument();
//            wordDocument.Load(docxFilePath);

//            // Create a new PDF document.
//            var pdfDocument = new GcPdfDocument();

//            // Use the layout engine to layout the Word document.
//            var layout = new GrapeCity.Documents.Word.Layout.GcWordDocumentLayout(wordDocument);

//            // Draw the laid out Word document pages onto the PDF document.
//            layout.Draw(pdfDocument.Pages, PageDescription.GetDefault());

//            // Save the PDF document to a byte array.
//            byte[] pdfBytes;
//            using (var ms = new MemoryStream())
//            {
//                pdfDocument.Save(ms);
//                pdfBytes = ms.ToArray();
//            }

//            // Return the PDF content.
//            return pdfBytes;
//        }

//    }
//}
