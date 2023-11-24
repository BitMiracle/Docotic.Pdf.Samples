using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CopyPageObjects
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

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