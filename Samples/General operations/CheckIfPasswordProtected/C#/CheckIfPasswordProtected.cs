using System.Text;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CheckIfPasswordProtected
    {
        public static void Main()
        {
            StringBuilder message = new StringBuilder();

            string[] documentsToCheck = { "encrypted.pdf", "jfif3.pdf" };
            foreach (string fileName in documentsToCheck)
            {
                bool passwordProtected = PdfDocument.IsPasswordProtected(@"Sample Data\" + fileName);
                if (passwordProtected)
                    message.AppendFormat("{0} - REQUIRES PASSWORD\r\n", fileName);
                else
                    message.AppendFormat("{0} - DOESN'T REQUIRE PASSWORD\r\n", fileName);
            }

            MessageBox.Show(message.ToString());
        }
    }
}
