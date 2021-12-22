using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class InitialView
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            useFitHeight();
            usePercentZoom();

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void useFitHeight()
        {
            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.InitialView = pdf.CreateView(0);
                pdf.InitialView.SetFitHeight();

                pdf.Save("FitHeight.pdf");
            }
        }

        private static void usePercentZoom()
        {
            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                pdf.InitialView = pdf.CreateView(0);
                pdf.InitialView.SetZoom(40);

                pdf.Save("Percent.pdf");
            }
        }
    }
}