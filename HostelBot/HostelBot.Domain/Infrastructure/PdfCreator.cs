using iTextSharp.text.pdf;
using iTextSharp.text;

namespace HostelBot.Domain.Infrastructure;

public static class PdfCreator
{
    public static byte[] CreatePdfFile(string html, string css = "")
    {
        byte[] bytes;
        
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

        return bytes;
    }
}