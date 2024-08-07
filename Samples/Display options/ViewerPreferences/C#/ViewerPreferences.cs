using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ViewerPreferences
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "ViewerPreferences.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.ViewerPreferences.CenterWindow = true;
                pdf.ViewerPreferences.FitWindow = true;
                pdf.ViewerPreferences.HideMenuBar = true;
                pdf.ViewerPreferences.HideToolBar = true;
                pdf.ViewerPreferences.HideWindowUI = true;

                pdf.Info.Title = "Test title";
                pdf.ViewerPreferences.DisplayTitle = true;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}