﻿using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace HostelBot.Domain.Infrastructure.Misc;

public static class PdfCreator
{
    private const string Css = "table, td { border: 1px solid #333; } thead, tfoot { background-color: #333; color: #fff; } ";

    public static byte[] CreatePdfFile(string html)
    {
        byte[] bytes;

        using var ms = new MemoryStream();
        using (var doc = new Document()) 
        using (var writer = PdfWriter.GetInstance(doc, ms)) {
            doc.Open();
                
            using (var msCss = new MemoryStream(Encoding.UTF8.GetBytes(Css))) 
            using (var msHtml = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss, Encoding.UTF8, new UnicodeFontFactory());
            
            doc.Close();
        }
            
        bytes = ms.ToArray();

        return bytes;
    }

    private class UnicodeFontFactory : FontFactoryImp
    {
        static UnicodeFontFactory()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private static readonly string fontPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
        private readonly BaseFont baseFont;

        public UnicodeFontFactory()
        {
            baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        }

        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
        {
            return new Font(baseFont, size, style, color);
        }
    }
}