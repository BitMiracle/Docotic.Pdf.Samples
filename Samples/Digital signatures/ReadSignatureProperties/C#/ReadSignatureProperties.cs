using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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

            var sb = new StringBuilder();
            using (var pdf = new PdfDocument(@"..\Sample Data\signed.pdf"))
            {
                PdfControl? control = pdf.GetControls().FirstOrDefault(c => c.Type == PdfWidgetType.Signature);
                if (control is null)
                {
                    Console.WriteLine("Document does not contain signature fields");
                    return;
                }

                PdfSignatureField field = (PdfSignatureField)control;
                sb.AppendFormat("Signature field is invisible: {0}\n", IsInvisible(field));

                PdfSignature? signature = field.Signature;
                if (signature is null)
                {
                    sb.AppendLine("Signature field does not have an associated signature");
                    Console.WriteLine(sb.ToString());
                    return;
                }

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
                sb.AppendFormat("Timestamp Authority URL: {0}\n", certificate.GetTimestampAuthorityUrl());

                sb.AppendLine();
                sb.AppendLine("== Issuer certificate:");

                PdfSignatureCertificate? issuer = contents.GetIssuerCertificateFor(certificate);
                if (issuer == null)
                {
                    sb.AppendLine("Not embedded in the PDF: true");

                    X509Certificate2? issuer2 = FindCertificateByIssuerName(certificate.Issuer);
                    if (issuer2 != null)
                    {
                        sb.AppendLine("Found in a local list of certificates: true");
                        sb.AppendFormat("Subject DN: {0}\n", issuer2.Subject);
                        sb.AppendFormat("Issuer DN: {0}\n", issuer2.Issuer);
                        sb.AppendFormat("Serial number: {0}\n", issuer2.SerialNumber);
                    }
                }
                else
                {
                    sb.AppendFormat("Subject DN: {0}\n", issuer.Subject.Name);
                    sb.AppendFormat("Issuer DN: {0}\n", issuer.Issuer.Name);
                    sb.AppendFormat("Serial number: {0}\n", issuer.SerialNumber);
                }
            }

            Console.WriteLine(sb.ToString());
        }

        private static bool IsInvisible(PdfSignatureField field)
        {
            return (field.Width == 0 && field.Height == 0) ||
                    field.Flags.HasFlag(PdfWidgetFlags.Hidden) ||
                    field.Flags.HasFlag(PdfWidgetFlags.NoView);
        }

        private static X509Certificate2? FindCertificateByIssuerName(X500DistinguishedName issuerName)
        {
            using var certificatesStore = new X509Store(StoreName.CertificateAuthority, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var searchType = X509FindType.FindBySubjectDistinguishedName;
            string findValue = issuerName.Name;
            X509Certificate2Collection matchingCertificates = certificatesStore.Certificates.Find(searchType,
                findValue, false);

            return matchingCertificates.Count > 0 ? matchingCertificates[0] : null;
        }
    }
}