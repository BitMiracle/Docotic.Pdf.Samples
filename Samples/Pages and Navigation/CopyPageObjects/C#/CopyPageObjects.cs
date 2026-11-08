using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CopyPageObjects
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "CopyPageObjects.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\BRAILLE CODES WITH TRANSLATION.pdf"))
            {
                using PdfDocument copy = pdf.Pages.CopyRange(0, 1);
                PdfPage sourcePage = copy.Pages[0];
                PdfPage copyPage = copy.Pages.Add();

                var options = new PdfObjectExtractionOptions
                {
                    // Set to true to convert searchable text into vector paths.
                    ExtractTextAsPath = false,

                    // Set to true to ignore marked content, such as layers and tagged PDF data.
                    //
                    // This sample does not preserve all structural information because page
                    // objects are copied to a different page. See the EditPageContent sample for
                    // an example of copying page objects within the same page while preserving all
                    // structural information.
                    FlattenMarkedContent = false,

                    // Usually, leave this option set to false to ensure proper handling of
                    // documents with transparency groups. Set to true for backward compatibility
                    // or when transparency handling is not a concern.
                    FlattenXObjects = false,
                };
                var copier = new PageObjectCopier(copy, options);
                copier.Copy(sourcePage, copyPage);

                copy.Pages.RemoveAt(0);

                copy.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}