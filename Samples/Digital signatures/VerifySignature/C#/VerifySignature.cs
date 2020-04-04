using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            using (PdfDocument pdf = new PdfDocument("Sample data/signed.pdf"))
            {
                PdfControl control = pdf.GetControls().FirstOrDefault(x => x is PdfSignatureField);
                PdfSignatureField field = (PdfSignatureField)control;
                PdfSignatureContents contents = field.Signature.Contents;
                sb.AppendFormat("Signed part is intact: {0}\n", contents.VerifyDigest());

                DateTime signingTime = field.Signature.SigningTime ?? DateTime.MinValue;
                sb.AppendFormat("Signed on: {0}\n\n", signingTime.ToShortDateString());

                if (contents.CheckHasEmbeddedOcsp())
                {
                    sb.AppendLine("Signature has OCSP embedded.");
                    checkRevocation(field, sb, PdfCertificateRevocationCheckMode.EmbeddedOcsp);
                }

                if (contents.CheckHasEmbeddedCrl())
                {
                    sb.AppendLine("Signature has CRL embedded.");
                    checkRevocation(field, sb, PdfCertificateRevocationCheckMode.EmbeddedCrl);
                }

                checkRevocation(field, sb, PdfCertificateRevocationCheckMode.OnlineOcsp);
                checkRevocation(field, sb, PdfCertificateRevocationCheckMode.OnlineCrl);
            }

            MessageBox.Show(sb.ToString(), "Verification result");
        }

        private static void checkRevocation(
            PdfSignatureField field, StringBuilder sb, PdfCertificateRevocationCheckMode mode)
        {
            PdfSignatureContents contents = field.Signature.Contents;
            DateTime signingTime = field.Signature.SigningTime ?? DateTime.MinValue;

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