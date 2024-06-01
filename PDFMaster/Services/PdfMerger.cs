using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace YourApp.Services
{
    public class PdfMerger
    {
        public void MergePdfs(string outputFilePath, params string[] pdfFiles)
        {
            using (FileStream stream = new FileStream(outputFilePath, FileMode.Create))
            {
                Document document = new Document();
                PdfCopy pdfCopy = new PdfCopy(document, stream);
                document.Open();

                foreach (var file in pdfFiles)
                {
                    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(file);
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        pdfCopy.AddPage(pdfCopy.GetImportedPage(reader, i));
                    }
                    reader.Close();
                }

                document.Close();
            }
        }
    }
}
