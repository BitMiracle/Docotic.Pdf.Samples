using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class GoToAction
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage secondPage = pdf.AddPage();
                secondPage.Canvas.DrawString(10, 300, "Go-to action target");

                PdfGoToAction action = pdf.CreateGoToPageAction(1, 300);
                PdfActionArea annotation = pdf.Pages[0].AddActionArea(10, 50, 100, 30, action);
                annotation.Border.Color = new PdfRgbColor(255, 0, 0);
                annotation.Border.DashPattern = new PdfDashPattern(new float[] { 3, 2 });
                annotation.Border.Width = 1;
                annotation.Border.Style = PdfMarkerLineStyle.Dashed;

                pdf.Save("GoToAction.pdf");
            }

            Process.Start("GoToAction.pdf");
        }
    }
}
