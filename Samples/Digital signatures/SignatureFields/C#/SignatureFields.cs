using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class SignatureFields
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "SignatureFields.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                var page = pdf.Pages[0];

                // creates a signature field with an auto-generated name
                page.AddSignatureField(100, 100, 200, 50);

                // creates a signature field with the specified name
                page.AddSignatureField("Signature2", 100, 200, 200, 50);

                // creates an invisible signature with the specified name
                // please note the width and height are zero for an invisible signature
                page.AddSignatureField("InvisibleSignature", 350, 100, 0, 0);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
