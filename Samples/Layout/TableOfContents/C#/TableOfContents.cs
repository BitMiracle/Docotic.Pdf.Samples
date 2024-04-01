using System;
using System.Diagnostics;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class TableOfContents
    {
        static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            const string PathToFile = "TableOfContents.pdf";
            PdfDocumentBuilder.Create().Generate(PathToFile, doc =>
            {
                doc.Pages(page =>
                {
                    page.Margin(50);

                    page.Content().Column(c =>
                    {
                        // Table of contents
                        c.Item().Text("Contents").Style(t => t.Heading2);
                        foreach (Section s in Sections)
                        {
                            string sectionName = s.Id;
                            c.Item().SectionLink(sectionName).Row(r =>
                            {
                                r.AutoItem().Text(s.Title);

                                PdfDashPattern pattern = new(new[] { 1.0, 3 });
                                r.RelativeItem().AlignBottom().PaddingHorizontal(3).LineHorizontal(1).DashPattern(pattern);

                                // You can also use these methods for different page number types:
                                // * t.LastPageNumber(sectionName)
                                // * t.PageCount(sectionName)
                                r.AutoItem().Text(t => t.FirstPageNumber(sectionName));
                            });
                        }

                        // Sections
                        foreach (Section s in Sections)
                        {
                            c.Item().PageBreak();

                            c.Item().Section(s.Id).Text(t =>
                            {
                                t.Line(s.Title).Style(t => t.Heading2);

                                t.Line(s.Content);
                            });
                        }
                    });
                });
            });

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }

        private static readonly Section[] Sections = new Section[]
        {
            new("extract-text",
                "Extract text from PDF document in C#",
                "Use Docotic.Pdf library to convert PDF documents to text in .NET. You can extract formatted text to parse structured data like tables.\n\n" +
                "You can also read PDF text with detailed information (position, font, color) about every text chunk. That allows you to search text in PDF documents and highlight found phrases.\n\n" +
                "Docotic.Pdf supports right-to-left and bidirectional text. You can use it to extract Arabic, Hebrew, and Persian text from PDF documents in .NET."),
            new("edit-documents",
                "Edit PDF documents in C#",
                "Docotic.Pdf is a powerful .NET PDF editor. You can compress PDF documents. It is possible to remove content. For example, potentially insecure content like actions, attachments, controls.\n\n" +
                "You can also edit page objects - replace images, change colors, remove or replace text in PDF.\n\n" +
                "Docotic.Pdf SDK allows you to split and merge PDF documents in just a few lines of code. And you can remove or reorder pages. With help of the library it's possible to impose PDF pages."),
            new("pdf-to-image",
                "Convert PDF to images in C#",
                "Our .NET PDF library allows you to save PDF pages as images. You can convert PDF pages to full size or thumbnail images in PNG, TIFF, and JPEG formats.\n\n" +
                "Or you can save PDF documents as multipage TIFF files. The library can produce bitonal and grayscale TIFF images.\n\n" +
                "It is also possible to print PDF documents in C# and VB.NET using Docotic.Pdf.\n\n" +
                "When needed, you can extract images from PDF documents."),
            new("html-to-pdf",
                "Convert HTML to PDF in C#",
                "Generate PDF from HTML using the free HTML to PDF add-on for Docotic.Pdf library.\n\n" +
                "The add-on uses Chromium during conversion, so the web standards compliance is great. You can produce PDF documents from the most complex HTML documents with scripts and styles.\n\n" +
                "For produced PDFs, it is possible to set up page size, margins, and orientation. The conversion can be delayed if needed. It is possible to convert password-protected HTML documents and documents with SSL errors."),
        };
    }

    record Section(string Id, string Title, string Content);
}
