using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class EnumerateLayers
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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
