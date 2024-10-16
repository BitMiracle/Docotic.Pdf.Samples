using System;
using System.Diagnostics;
using System.Linq;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemoveWidgets
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var removeFromPageOutputName = "RemoveWidgetFromPage.pdf";
            var removeAllOutputName = "RemoveWidgets.pdf";
            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                pdf.Pages[0].Widgets.RemoveAt(1);
                pdf.Save(removeFromPageOutputName);

                foreach (PdfWidget widget in pdf.GetWidgets().ToArray())
                    pdf.RemoveWidget(widget);

                pdf.Save(removeAllOutputName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(removeFromPageOutputName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(removeAllOutputName) { UseShellExecute = true });
        }
    }
}
