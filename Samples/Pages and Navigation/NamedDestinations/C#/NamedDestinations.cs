using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class NamedDestinations
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            var outputName = "NamedDestinations.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\links-with-named-destinations.pdf"))
            {
                // Read existing named views (destinations)
                foreach (KeyValuePair<string, PdfDocumentView> kvp in pdf.SharedViews)
                    Console.WriteLine($"{kvp.Key} => {viewToString(kvp.Value, pdf)}");

                // Add another named view
                const string ViewName = "Test destination name";
                const int TargetPageIndex = 2;
                PdfDocumentView view = pdf.CreateView(TargetPageIndex);
                view.SetZoom(new PdfPoint(0, 300), 0);
                pdf.SharedViews.Add(ViewName, view);

                // Use the created named view in a go-to action
                PdfGoToAction gotoAction = pdf.CreateGoToPageAction(ViewName);
                PdfActionArea area = pdf.Pages[0].AddActionArea(200, 50, 30, 20, gotoAction);
                area.Border.Color = new PdfRgbColor(255, 0, 0);
                area.Border.Width = 2;

                // Add some text to the target position
                pdf.Pages[TargetPageIndex].Canvas.DrawString(0, 300, "From the new named destination");

                pdf.Save(outputName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputName) { UseShellExecute = true });
        }

        private static string viewToString(PdfDocumentView view, PdfDocument pdf)
        {
            if (view.Page != null)
                return $"page index {pdf.Pages.IndexOf(view.Page)}";

            return $"page index {view.PageIndex}";
        }
    }
}
