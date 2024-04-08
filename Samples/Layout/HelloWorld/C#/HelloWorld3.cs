using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class HelloWorld3
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
    }
}
