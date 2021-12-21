using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ViewerPreferences
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "ViewerPreferences.pdf";

            using (PdfDocument pdf = new PdfDocument())
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
        }
    }
}