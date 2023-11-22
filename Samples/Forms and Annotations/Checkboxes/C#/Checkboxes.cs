using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class Checkboxes
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Checkboxes.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                page.AddCheckBox(10, 50, 120, 15, "Check box");

                PdfCheckBox readonlyCheckBox = page.AddCheckBox(10, 100, 120, 15, "Read-only check box");
                readonlyCheckBox.ReadOnly = true;
                readonlyCheckBox.Checked = true;

                PdfCheckBox checkBoxWithBorder = page.AddCheckBox(10, 150, 140, 15, "Check box with colored border");
                checkBoxWithBorder.Border.Color = new PdfCmykColor(40, 90, 70, 3);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
