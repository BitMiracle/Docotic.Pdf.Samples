using System;
using System.Diagnostics;

using SignByMultiplePeople;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignByMultiplePeople
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            // IMPORTANT:
            // Please replace keystore paths and passwords in the following declarations with
            // your own .p12 or .pfx path and password. Without the changes the sample will not work.
            // For testing purposes you can use the same keystore for all signers.
            var signers = new Credentials[]
            {
                new("Frida Palacios", @"keystore.p12", "password"), // request author
                new("Dax Yu", @"keystore.p12", "password"), // registered by
                new("Lana Conway", @"keystore.p12", "password"), // approved by
                new("Paulina Mann", @"keystore.p12", "password"), // executed by
            };

            // Start by filling in the requester name. Then sign as an author, disallowing
            // all further changes to the document except the filling and signing.
            var SignByMultiplePeopleStep1 = "SignByMultiplePeopleStep1.pdf";
            using (var sourcePdf = new PdfDocument(@"..\Sample Data\shipping-request.pdf"))
            {
                SetText(sourcePdf, "goods", "A dog, a cat, and a small hat. That's it.");
                SetText(sourcePdf, "requestedByName", signers[0].Name);

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep1,
                    "requestedBySignature",
                    signers[0],
                    PdfSignatureType.AuthorFormFillingAllowed,
                    "I created this request",
                    "A nice spot",
                    "email@example.com");
            }

            // Open the document signed by the requester. Then sign as the registerer of the request.
            var SignByMultiplePeopleStep2 = "SignByMultiplePeopleStep2.pdf";
            using (var sourcePdf = new PdfDocument(SignByMultiplePeopleStep1))
            {
                SetText(sourcePdf, "registeredByName", signers[1].Name);

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep2,
                    "registeredBySignature",
                    signers[1],
                    PdfSignatureType.Approval,
                    "I registered this request",
                    "My worklace",
                    "email@example2.com");
            }

            // Open the document signed by the reqisterer. Then sign as the person who approved the request.
            var SignByMultiplePeopleStep3 = "SignByMultiplePeopleStep3.pdf";
            using (var sourcePdf = new PdfDocument(SignByMultiplePeopleStep2))
            {
                SetText(sourcePdf, "approvedByName", signers[2].Name);

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep3,
                    "approvedBySignature",
                    signers[2],
                    PdfSignatureType.Approval,
                    "I approved this request",
                    "My worklace",
                    "approvals@example2.com");
            }

            // Open the approved and signed document. Then sign as the person who executed the request.
            var SignByMultiplePeopleStep4 = "SignByMultiplePeopleStep4.pdf";
            using (var sourcePdf = new PdfDocument(SignByMultiplePeopleStep3))
            {
                SetText(sourcePdf, "executedByName", signers[3].Name);

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep4,
                    "executedBySignature",
                    signers[3],
                    PdfSignatureType.Approval,
                    "I approved this request",
                    "My worklace",
                    "office@example2.com");
            }

            // Uncomment if you would like to open previous step result, too.
            //OpenResult(SignByMultiplePeopleStep1);
            //OpenResult(SignByMultiplePeopleStep2);
            //OpenResult(SignByMultiplePeopleStep3);
            OpenResult(SignByMultiplePeopleStep4);
            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void SignAndSave(
            PdfDocument source,
            string destFileName,
            string signatureFieldName,
            Credentials credentials,
            PdfSignatureType signatureType,
            string reason,
            string location,
            string contactInfo)
        {
            var field = source.GetControl(signatureFieldName) as PdfSignatureField;
            var signingOptions = new PdfSigningOptions(credentials.Keystore, credentials.Password)
            {
                DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                Format = PdfSignatureFormat.Pkcs7Detached,
                Field = field,
                Type = signatureType,
                Reason = reason,
                Location = location,
                ContactInfo = contactInfo,
            };

            var saveOptions = new PdfSaveOptions
            {
                // It is extremely important to write the resulting file incrementally.
                // Otherwise, the signature in the source file will be invalidated.
                WriteIncrementally = true,
                UseObjectStreams = false,
            };

            source.SignAndSave(signingOptions, destFileName, saveOptions);
        }

        private static void SetText(PdfDocument pdf, string textBoxName, string text)
        {
            if (pdf.GetControl(textBoxName) is PdfTextBox textBox)
                textBox.Text = text;
            else
                throw new ArgumentException($"Unable to find a text box by name = '{textBoxName}'", nameof(textBoxName));
        }

        private static void OpenResult(string path)
        {
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }
    }
}