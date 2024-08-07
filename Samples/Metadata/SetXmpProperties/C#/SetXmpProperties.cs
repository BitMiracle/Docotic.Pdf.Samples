using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SetXmpProperties
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "SetXmpProperties.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.Metadata.DublinCore.Creators = new XmpArray(XmpArrayType.Ordered);
                pdf.Metadata.DublinCore.Creators.Values.Add(new XmpString("me"));
                pdf.Metadata.DublinCore.Creators.Values.Add(new XmpString("Docotic.Pdf"));

                pdf.Metadata.DublinCore.Format = new XmpString("application/pdf");

                pdf.Metadata.Pdf.Producer = new XmpString("me too!");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
