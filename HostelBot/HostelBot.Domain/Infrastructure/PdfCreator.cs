using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace HostelBot.Domain.Infrastructure;

public static class PdfCreator
{
    public static void CreatePdfFile(string path, string html, string css = "")
    {
        Byte[] bytes;
        
        using (var ms = new MemoryStream()) {
            using (var doc = new Document()) {
                using (var writer = PdfWriter.GetInstance(doc, ms)) {
                    doc.Open();
                    
                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css))) {
                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html))) {
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                        }
                    }
                    
                    doc.Close();
                }
            }
            
            bytes = ms.ToArray();
        }
        
        File.WriteAllBytes(path, bytes);
    }
}