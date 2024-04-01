using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ThreeDAnnotations
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.
            
            const string PathToFile = "3dAnnotations.pdf";
            
            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                var bounds = new PdfRectangle(10, 80, 400, 400);
                Pdf3dAnnotation annot = page.Add3dAnnotation(bounds, @"..\Sample Data\dice.u3d");
                annot.Activation.ActivationMode = PdfRichMediaActivationMode.OnPageOpen;

                pdf.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
