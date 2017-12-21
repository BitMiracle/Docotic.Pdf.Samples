using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class MergeDocuments
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "MergeDocuments.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.Open("Sample data/form.pdf");
                pdf.Append("Sample data/jfif3.pdf");

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}