using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FlattenFormFields
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.
            
            const string pathToFile = "FlattenFormFields.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                pdf.FlattenControls();

                // Alternatively, you can:
                // 1. Flatten controls and annotations
                // pdf.FlattenWidgets();
                //
                // 2. Flatten individual controls
                // foreach (PdfControl control in pdf.GetControls())
                //     control.Flatten();

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
