using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ReadSignatureProperties
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
                PdfControl control = pdf.GetControls().FirstOrDefault(c => c.Type == PdfWidgetType.Signature);
                if (control == null)
                {
                    MessageBox.Show("Document does not contain signature fields", "Signature Info");
                    return;
                }

                PdfSignatureField field = (PdfSignatureField)control;
                sb.AppendFormat("Signature field is invisible: {0}\n", isInvisible(field));

                PdfSignature signature = field.Signature;
                sb.AppendFormat("Signed by: {0}\n", signature.Name);
                sb.AppendFormat("Signing time: {0}\n", signature.SigningTime);
                sb.AppendFormat("Signed at: {0}\n", signature.Location);
                sb.AppendFormat("Reason for signing: {0}\n", signature.Reason);
                sb.AppendFormat("Signer's contact: {0}\n", signature.ContactInfo);

                PdfSignatureContents contents = signature.Contents;
                sb.AppendFormat("Has OCSP embedded: {0}\n", contents.CheckHasEmbeddedOcsp());
                sb.AppendFormat("Has CRL embedded: {0}\n", contents.CheckHasEmbeddedCrl());

                PdfSignatureCertificate certificate = contents.GetSigningCertificate();
                sb.AppendLine();
                sb.AppendLine("== Signing certificate:");
                sb.AppendFormat("Name: {0}\n", certificate.Name);
                sb.AppendFormat("Algorithm: {0}\n", certificate.AlgorithmName);
                sb.AppendFormat("Subject DN: {0}\n", certificate.Subject.Name);
                sb.AppendFormat("Issuer DN: {0}\n", certificate.Issuer.Name);
                sb.AppendFormat("Serial number: {0}\n", certificate.SerialNumber);
                sb.AppendFormat("Valid from {0} up to {1}\n", certificate.ValidFrom, certificate.ValidUpto);

                PdfSignatureCertificate issuer = contents.GetIssuerCertificateFor(certificate);
                sb.AppendLine();
                sb.AppendLine("== Issuer certificate:");
                sb.AppendFormat("Subject DN: {0}\n", issuer.Subject.Name);
                sb.AppendFormat("Issuer DN: {0}\n", issuer.Issuer.Name);
                sb.AppendFormat("Serial number: {0}\n", issuer.SerialNumber);
            }

            MessageBox.Show(sb.ToString(), "Signature Info");
        }

        private static bool isInvisible(PdfSignatureField field)
        {
            return (field.Width == 0 && field.Height == 0) ||
                    field.Flags.HasFlag(PdfWidgetFlags.Hidden) ||
                    field.Flags.HasFlag(PdfWidgetFlags.NoView);
        }
    }
}