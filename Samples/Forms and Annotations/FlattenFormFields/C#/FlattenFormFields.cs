using System.Diagnostics;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FlattenFormFields
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            const string pathToFile = "FlattenFormFields.pdf";

            using (PdfDocument pdf = new PdfDocument("Sample Data/form.pdf"))
            {
                pdf.FlattenControls();
                
                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
