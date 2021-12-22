using System;
using System.Linq;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CheckIfPasswordProtected
    {
        public static void Main()
        {
            StringBuilder message = new StringBuilder();

            string[] documentsToCheck = { "encrypted.pdf", "jfif3.pdf", "public-key-encrypted.pdf" };
            foreach (string fileName in documentsToCheck)
            {
                PdfEncryptionInfo info = PdfDocument.GetEncryptionInfo(@"..\Sample Data\" + fileName);
                if (info == null)
                {
                    message.AppendFormat("{0} - is not encrypted\r\n", fileName);
                }
                else if (info is PdfStandardEncryptionInfo standardInfo)
                {
                    if (standardInfo.RequiresPasswordToOpen)
                        message.AppendFormat("{0} - is encrypted and requires password\r\n", fileName);
                    else
                        message.AppendFormat("{0} - is encrypted and doesn't require password\r\n", fileName);
                }
                else if (info is PdfPublicKeyEncryptionInfo publicKeyInfo)
                {
                    message.AppendFormat("{0} - is encrypted and requires one of the certificates: ", fileName);
                    string[] serialNumbers = publicKeyInfo.Recipients.Select(r => r.SerialNumber).ToArray();
                    message.Append(string.Join(", ", serialNumbers));
                }
            }

            Console.WriteLine(message.ToString());
        }
    }
}
