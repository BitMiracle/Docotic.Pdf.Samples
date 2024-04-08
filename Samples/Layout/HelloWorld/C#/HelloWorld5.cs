using System;
using System.IO;

using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class HelloWorld5
    {
        public static void CreatePdf()
        {
            PdfDocumentBuilder.Create().Generate("hello.pdf", BuildDocument);
        }

        static void BuildDocument(Document doc)
        {
            doc.Typography(t =>
            {
                t.Document = doc.TextStyleWithFont(SystemFont.Family("Calibri"));
            });

            var imageFile = new FileInfo(@"..\Sample Data\red-flowers-at-butterfly-world.jpg");
            var image = doc.Image(imageFile);

            doc.Pages(pages => {
                BuildPages(pages);

                pages.Content().Column(c =>
                {
                    BuildTextContent(c.Item());
                    BuildImageContent(c.Item(), image);
                });
            });
        }

        static void BuildImageContent(LayoutContainer content, Image image)
        {
            content.AlignCenter()
                .PaddingVertical(20)
                .Image(image);
        }

        static void BuildPages(PageLayout pages)
        {
            pages.Size(PdfPaperSize.A6);
            pages.Margin(10);

            BuildPagesHeader(pages.Header());
            BuildPagesFooter(pages.Footer());
        }

        static void BuildTextContent(LayoutContainer content)
        {
            var colorAccented = new PdfRgbColor(56, 194, 10);
            var styleAccented = TextStyle.Parent.FontColor(colorAccented);

            content.Text(t =>
            {
                t.Span("Hello, ").Style(styleAccented);
                t.Span("world!").Style(styleAccented.Underline());
            });
        }

        static void BuildPagesHeader(LayoutContainer header)
        {
            header.TextStyle(TextStyle.Parent.FontSize(8))
                .AlignRight()
                .Text(t =>
                {
                    t.Line($"Created by: {Environment.UserName}");
                    t.Line($"Date: {DateTime.Now}");
                });
        }

        static void BuildPagesFooter(LayoutContainer footer)
        {
            footer.Height(20)
                .Background(new PdfGrayColor(95))
                .AlignCenter()
                .Text(t => t.CurrentPageNumber());
        }
    }
}
