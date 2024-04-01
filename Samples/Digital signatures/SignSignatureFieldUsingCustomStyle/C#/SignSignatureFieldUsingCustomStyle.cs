using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignSignatureFieldUsingCustomStyle
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string outputFileName = "SignSignatureFieldUsingCustomStyle.pdf";
            using (var pdf = new PdfDocument(@"..\Sample Data\SignatureFields.pdf"))
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change the sample will not work.

                if (pdf.GetControl("Control") is not PdfSignatureField field)
                {
                    Console.WriteLine("Cannot find a signature field");
                    return;
                }

                field.BackgroundColor = new PdfGrayColor(80);

                var options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.Pkcs7Detached,
                    Field = field,
                    Reason = "Testing field styles",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                PdfSignatureAppearanceOptions appearance = options.Appearance;
                appearance.IncludeDate = false;
                appearance.IncludeDistinguishedName = false;

                appearance.Image = pdf.AddImage(@"..\Sample Data\ammerland.jpg");
                appearance.Font = pdf.AddFont(PdfBuiltInFont.Courier);
                appearance.FontSize = 0; // calculate font size automatically
                appearance.FontColor = new PdfRgbColor(0, 0, 255);
                appearance.TextAlignment = PdfSignatureTextAlignment.Right;

                appearance.NameLabel = "Digital signiert von";
                appearance.ReasonLabel = "Grund:";
                appearance.LocationLabel = "Ort:";

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}