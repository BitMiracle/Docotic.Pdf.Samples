using System;

using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class HelloWorld4
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
            doc.Pages(BuildPages);
        }

        static void BuildPages(PageLayout pages)
        {
            pages.Size(PdfPaperSize.A6);
            pages.Margin(10);

            BuildPagesHeader(pages.Header());
            BuildPagesFooter(pages.Footer());

            BuildTextContent(pages.Content());
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
