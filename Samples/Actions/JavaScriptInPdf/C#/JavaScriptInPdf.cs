using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class JavascriptInPdf
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            // We have an article that provides more information about what this sample code does.
            // Read it on Medium: https://medium.com/@bitmiracle/javascript-in-pdf-7d738d6c6c09

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            //CreateNamesDocument.Run();

            HelloWorld();
            ValidateData.Run();
            SynchronizeData.Run();

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void HelloWorld()
        {
            string pathToFile = "HelloWorld.pdf";
            using (var pdf = new PdfDocument())
            {
                pdf.OnOpenDocument = pdf.CreateJavaScriptAction("app.alert(\"Hello, Medium!\", 3);");

                pdf.Save(pathToFile);
            }

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
