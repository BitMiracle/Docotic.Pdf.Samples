using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemoveWidgets
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var removeFromPageOutputName = "RemoveWidgetFromPage.pdf";
            var removeAllOutputName = "RemoveWidgets.pdf";
            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                pdf.Pages[0].Widgets.RemoveAt(1);
                pdf.Save(removeFromPageOutputName);

                foreach (PdfWidget widget in pdf.GetWidgets())
                    pdf.RemoveWidget(widget);

                pdf.Save(removeAllOutputName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(removeFromPageOutputName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(removeAllOutputName) { UseShellExecute = true });
        }
    }
}
