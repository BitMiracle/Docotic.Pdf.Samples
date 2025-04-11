using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddLtvInfoToSignature
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string outputFileName = "AddLtvInfoToSignature.pdf";
            using (var pdf = new PdfDocument(@"..\Sample Data\signed-cades.pdf"))
            {
                pdf.AddLtvInfo();

                var timestampOptions = new PdfSignatureTimestampOptions
                {
                    AuthorityUrl = new Uri("http://timestamp.digicert.com"),
                };
                var incrementalOptions = new PdfSaveOptions()
                {
                    WriteIncrementally = true,
                };
                pdf.TimestampAndSave(timestampOptions, outputFileName, incrementalOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}