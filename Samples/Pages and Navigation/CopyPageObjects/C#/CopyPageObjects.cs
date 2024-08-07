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
                using PdfDocument copy = pdf.CopyPages(0, 1);
                PdfPage sourcePage = copy.Pages[0];
                PdfPage copyPage = copy.AddPage();

                var options = new PdfObjectExtractionOptions
                {
                    // Set this option to true if you need to convert searchable text to vector paths
                    ExtractTextAsPath = false,

                    // Set this option to true to ignore marked content (such as layers or tagged data)
                    //
                    // This sample does not preserve all structure information because page objects are
                    // copied to another page. Look at the EditPageContent sample that shows how to copy
                    // page objects to the same page and preserve all structure information.
                    FlattenMarkedContent = false,

                    // Usually, you need to set this option to false. That allows to properly handle
                    // documents with transparency groups.
                    // The value = true is recommended for backward compatibility or if you do not
                    // worry about transparency.
                    FlattenXObjects = false,
                };
                var copier = new PageObjectCopier(copy, options);
                copier.Copy(sourcePage, copyPage);

                copy.RemovePage(0);

                copy.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}