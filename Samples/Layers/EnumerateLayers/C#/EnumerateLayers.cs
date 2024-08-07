using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class EnumerateLayers
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using var pdf = new PdfDocument(@"..\Sample Data\BorderPinksOranges.pdf");
            foreach (PdfLayer? layer in pdf.Layers)
            {
                if (layer == null)
                    continue;

                string message = string.Format("Name = {0}\nVisible = {1}\nIntents = ",
                    layer.Name, layer.Visible);

                foreach (PdfLayerIntent intent in layer.GetIntents())
                {
                    message += intent.ToString();
                    message += " ";
                }

                Console.WriteLine("Layer Info:");
                Console.WriteLine(message);
                Console.WriteLine();
            }
        }
    }
}
