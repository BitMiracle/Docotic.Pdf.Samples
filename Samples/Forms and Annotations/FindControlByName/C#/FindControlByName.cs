using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FindControlByName
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            const string pathToFile = "FindControlByName.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                if (pdf.GetControl("email") is PdfTextBox emailTextBox)
                    emailTextBox.Text = "support@bitmiracle.com";

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
