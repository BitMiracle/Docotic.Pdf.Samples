using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class HelloWorld2
    {
        public static void CreatePdf()
        {
            PdfDocumentBuilder.Create().Generate("hello.pdf", BuildDocument);
        }

        static void BuildDocument(Document doc)
        {
            doc.Pages(BuildPages);
        }

        static void BuildPages(PageLayout pages)
        {
            pages.Content().Text("Hello, world!");
        }
    }
}
