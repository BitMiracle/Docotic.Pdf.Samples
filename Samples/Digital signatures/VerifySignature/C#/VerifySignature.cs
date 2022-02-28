using System;
using System.Linq;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class VerifySignature
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            StringBuilder sb = new StringBuilder();
            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\signed.pdf"))
            {
                PdfControl field = pdf.GetControls().FirstOrDefault(c => c.Type == PdfWidgetType.Signature);
                if (field == null)
                {
                    Console.WriteLine("Document does not contain signature fields");
                    return;
                }

                PdfSignature signature = ((PdfSignatureField)field).Signature;
                PdfSignatureContents contents = signature.Contents;
                sb.AppendFormat("Signed part is intact: {0}\n", contents.VerifyDigest());

                DateTime signingTime = signature.SigningTime ?? DateTime.MinValue;
                sb.AppendFormat("Signed on: {0}\n", signingTime.ToShortDateString());

                var timestampToken = contents.GetTimestampToken();
                if (timestampToken != null)
                {
                    sb.AppendFormat("Embedded timestamp: {0}\n", timestampToken.GenerationTime);
                    if (timestampToken.TimestampAuthority != null)
                        sb.AppendFormat("Timestamp authority: {0}\n", timestampToken.TimestampAuthority.Name);
                    sb.AppendFormat("Timestamp is intact: {0}\n\n", contents.VerifyTimestamp());
                }
                else
                {
                    sb.AppendLine();
                }

                if (contents.CheckHasEmbeddedOcsp())
                {
                    sb.AppendLine("Signature has OCSP embedded.");
                    checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.EmbeddedOcsp);
                }

                if (contents.CheckHasEmbeddedCrl())
                {
                    sb.AppendLine("Signature has CRL embedded.");
                    checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.EmbeddedCrl);
                }

                checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.OnlineOcsp);
                checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.OnlineCrl);
            }

            Console.WriteLine(sb.ToString());
        }

        private static void checkRevocation(PdfSignature signature, StringBuilder sb, PdfCertificateRevocationCheckMode mode)
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