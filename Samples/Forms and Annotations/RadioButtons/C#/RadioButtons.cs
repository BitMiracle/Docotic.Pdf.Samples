using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RadioButtons
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "RadioButtons.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                page.Canvas.DrawString(10, 50, "Docotic.Pdf library is written using: ");

                PdfRadioButton radioButton = page.AddRadioButton("languages", 10, 60, 100, 15, "C#");
                radioButton.ExportValue = "csharp";
                radioButton.Checked = true;

                PdfRadioButton radioButton2 = page.AddRadioButton("languages", 10, 80, 100, 15, "Python");
                radioButton2.ExportValue = "python";
                radioButton2.Checked = false;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
