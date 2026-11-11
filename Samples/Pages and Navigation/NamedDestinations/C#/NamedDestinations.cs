using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class NamedDestinations
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var outputName = "NamedDestinations.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\links-with-named-destinations.pdf"))
            {
                // Read the existing named views (destinations).
                foreach (var entry in pdf.SharedViews)
                    Console.WriteLine($"{entry.Name} => {ViewToString(entry.View, pdf)}");

                // Add another named view.
                const string ViewName = "Test destination name";
                const int TargetPageIndex = 2;
                PdfDocumentView view = pdf.CreateView(TargetPageIndex);
                view.SetZoom(new PdfPoint(0, 300), 0);
                pdf.SharedViews.Add(ViewName, view);

                // Use the newly created named view in a go-to action.
                PdfGoToAction gotoAction = pdf.CreateGoToPageAction(ViewName);
                PdfActionArea area = pdf.Pages[0].AddActionArea(200, 50, 30, 20, gotoAction);
                area.Border.Color = new PdfRgbColor(255, 0, 0);
                area.Border.Width = 2;

                // Add some text at the target position.
                pdf.Pages[TargetPageIndex].Canvas.DrawString(0, 300, "You have reached the new named destination");

                pdf.Save(outputName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputName) { UseShellExecute = true });
        }

        private static string ViewToString(PdfDocumentView? view, PdfDocument pdf)
        {
            if (view is null)
                return "undefined";

            if (view.Page != null)
                return $"page index {FindPageIndex(pdf, view.Page)}";

            return $"page index {view.PageIndex}";
        }

        private static int FindPageIndex(PdfDocument pdf, PdfPage page)
        {
            var pageDictionary = page?.Dictionary;
            if (pageDictionary is null)
                return -1;

            var pages = pdf.Pages;
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].Dictionary == pageDictionary)
                    return i;
            }

            return -1;
        }
    }
}
