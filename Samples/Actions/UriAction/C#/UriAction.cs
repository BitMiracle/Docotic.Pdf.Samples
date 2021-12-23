using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class UriAction
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfUriAction uriAction = pdf.CreateHyperlinkAction(new Uri("http://www.google.com"));
                PdfActionArea annotation = pdf.Pages[0].AddActionArea(10, 50, 100, 100, uriAction);
                annotation.Border.Color = new PdfRgbColor(0, 0, 0);
                annotation.Border.DashPattern = new PdfDashPattern(new float[] { 3, 2 });
                annotation.Border.Width = 1;
                annotation.Border.Style = PdfMarkerLineStyle.Dashed;

                pdf.Save("UriAction.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
