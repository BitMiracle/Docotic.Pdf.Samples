using System;
using System.Diagnostics;
using System.Linq;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FlattenFormFields
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string pathToFile = "FlattenFormFields.pdf";

            bool flattenWidgets = false;
            bool flattenSomeOfTheControls = false;

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                pdf.FlattenControls();

                // Alternatively, you can:

                if (flattenWidgets)
                {
                    // 1. Flatten both controls and annotations
                    pdf.FlattenWidgets();
                }

                if (flattenSomeOfTheControls)
                {
                    // 2. Flatten individual controls
                    foreach (PdfControl control in pdf.GetControls().ToArray())
                        control.Flatten();
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
