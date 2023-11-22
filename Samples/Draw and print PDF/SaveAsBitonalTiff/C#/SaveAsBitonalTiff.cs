using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SaveAsBitonalTiff
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string outputDocumentPath = "SaveAsBitonalTiff.tiff";
            string outputPagePath = "SaveAsBitonalTiff_page0.tiff";

            using (var pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                PdfDrawOptions options = PdfDrawOptions.Create();
                options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                options.HorizontalResolution = 300;
                options.VerticalResolution = 300;

                // specify bitonal TIFF as the desired output compression
                options.Compression = ImageCompressionOptions.CreateTiff()
                    .SetBitonal();

                // save one page
                pdf.Pages[0].Save(outputPagePath, options);

                // save the whole document as multipage TIFF
                pdf.SaveAsTiff(outputDocumentPath, options);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputPagePath) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(outputDocumentPath) { UseShellExecute = true });
        }
    }
}
