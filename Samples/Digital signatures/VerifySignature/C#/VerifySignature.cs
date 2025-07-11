﻿using System;
using System.Linq;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class VerifySignature
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var sb = new StringBuilder();
            using var pdf = new PdfDocument(@"..\Sample Data\signed.pdf");

            PdfControl? field = pdf.GetControls().FirstOrDefault(c => c.Type == PdfWidgetType.Signature);
            if (field is null)
            {
                Console.WriteLine("Document does not contain signature fields");
                return;
            }

            PdfSignature? signature = ((PdfSignatureField)field).Signature;
            if (signature is null)
            {
                Console.WriteLine("Signature field does not have an associated signature");
                return;
            }

            PdfSignatureContents contents = signature.Contents;
            sb.AppendFormat("Signed part is intact: {0}\n", contents.VerifyDigest());

            DateTime signingTime = signature.SigningTime ?? DateTime.MinValue;
            sb.AppendFormat("Signed on: {0}\n\n", signingTime.ToShortDateString());

            var timestampToken = contents.GetTimestampToken();
            if (timestampToken != null)
            {
                sb.AppendFormat("Is document timestamp: {0}\n", contents.IsDocumentTimestamp);
                sb.AppendFormat("Embedded timestamp: {0}\n", timestampToken.GenerationTime);
                if (timestampToken.TimestampAuthority != null)
                    sb.AppendFormat("Timestamp authority: {0}\n", timestampToken.TimestampAuthority.Name);

                if (!contents.IsDocumentTimestamp)
                    sb.AppendFormat("Timestamp is intact: {0}\n", contents.VerifyTimestamp());
            }

            sb.AppendLine();

            if (contents.CheckHasEmbeddedOcsp())
            {
                sb.AppendLine("Signature has OCSP embedded.");
                CheckRevocation(signature, sb, PdfCertificateRevocationCheckMode.EmbeddedOcsp);
            }

            if (contents.CheckHasEmbeddedCrl())
            {
                sb.AppendLine("Signature has CRL embedded.");
                CheckRevocation(signature, sb, PdfCertificateRevocationCheckMode.EmbeddedCrl);
            }

            CheckRevocation(signature, sb, PdfCertificateRevocationCheckMode.OnlineOcsp);
            CheckRevocation(signature, sb, PdfCertificateRevocationCheckMode.OnlineCrl);

            Console.WriteLine(sb.ToString());
        }

        private static void CheckRevocation(PdfSignature signature, StringBuilder sb, PdfCertificateRevocationCheckMode mode)
        {
            PdfSignatureContents contents = signature.Contents;
            DateTime signingTime = signature.SigningTime ?? DateTime.MinValue;

            foreach (DateTime time in new DateTime[] { signingTime, DateTime.UtcNow })
            {
                bool revoked = contents.CheckIfRevoked(mode, time);
                string status = revoked ? "Revoked" : "Valid";
                string date = time.ToShortDateString();
                sb.AppendFormat("Checking using {0} mode: {1} on {2}\n", mode, status, date);
            }

            sb.AppendLine();
        }
    }
}