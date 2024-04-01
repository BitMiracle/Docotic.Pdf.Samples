using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemovePaths
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.
            
            const string PathToFile = "RemovePaths.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                PdfPage page = pdf.Pages[0];
                page.RemovePaths(
                    path =>
                    {
                        // remove the form header filled with orange (255, 127, 64)
                        if (path.PaintMode == PdfDrawMode.Stroke)
                            return false;

                        return 
                            path.Brush.Color is PdfRgbColor fillColor &&
                            fillColor.R == 255 &&
                            fillColor.G == 127 &&
                            fillColor.B == 64
                        ;
                    }
                );

                pdf.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
