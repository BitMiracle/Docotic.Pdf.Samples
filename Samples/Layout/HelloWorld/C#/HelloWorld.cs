using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class HelloWorld
    {
        public static void CreatePdf()
        {
            PdfDocumentBuilder.Create().Generate("hello.pdf", doc => doc.Pages(page =>
            {
                page.Content().Text("Hello, world!");
            }));
        }
    }
}
