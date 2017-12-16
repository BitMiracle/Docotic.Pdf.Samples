using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CompressWithSaveOptions
    {
        public static void Main()
        {
            const string originalFile = @"Sample Data\BRAILLE CODES WITH TRANSLATION.pdf";
            const string compressedFile = "CompressWithSaveOptions.pdf";

            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument(originalFile))
            {
                pdf.SaveOptions.Compression = PdfCompression.Flate;
                pdf.SaveOptions.UseObjectStreams = true;
                pdf.SaveOptions.RemoveUnusedObjects = true;
                pdf.SaveOptions.OptimizeIndirectObjects = true;
                pdf.SaveOptions.WriteWithoutFormatting = true;

                pdf.Save(compressedFile);
            }

            string message = string.Format(
                "Original file size: {0} bytes;\r\nCompressed file size: {1} bytes",
                new FileInfo(originalFile).Length,
                new FileInfo(compressedFile).Length
            );
            MessageBox.Show(message);

            Process.Start(compressedFile);
        }
    }
}
