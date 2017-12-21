using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CompareDocuments
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string originalFile = @"Sample Data\Link.pdf";
            using (PdfDocument first = new PdfDocument(originalFile))
                first.Save("first.pdf");

            using (PdfDocument second = new PdfDocument(originalFile))
                second.Save("second.pdf");

            using (PdfDocument third = new PdfDocument(originalFile))
            {
                third.Pages[0].Canvas.DrawString("Hello");
                third.Save("third.pdf");
            }

            StringBuilder message = new StringBuilder();
            bool equals = PdfDocument.DocumentsAreEqual("first.pdf", "second.pdf", "");
            message.AppendLine("first.pdf equals to second.pdf?\r\n" + (equals ? "Yes" : "No"));

            message.AppendLine();

            equals = PdfDocument.DocumentsAreEqual("first.pdf", "third.pdf", "");
            message.AppendLine("first.pdf equals to third.pdf?\r\n" + (equals ? "Yes" : "No"));

            MessageBox.Show(message.ToString());
        }
    }
}
