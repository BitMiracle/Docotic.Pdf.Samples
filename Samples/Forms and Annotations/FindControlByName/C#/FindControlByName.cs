using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FindControlByName
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string pathToFile = "FindControlByName.pdf";

            using (PdfDocument pdf = new PdfDocument(@"Sample Data\form.pdf"))
            {
                PdfTextBox emailTextBox = pdf.GetControl("email") as PdfTextBox;
                Debug.Assert(emailTextBox != null);

                emailTextBox.Text = "support@bitmiracle.com";

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
