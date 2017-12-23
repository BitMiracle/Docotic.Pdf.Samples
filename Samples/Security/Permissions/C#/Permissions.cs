using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Permissions
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Permissions.pdf";

            using (PdfDocument pdf = new PdfDocument("Sample data/form.pdf"))
            {
                // an owner password should be set in order to use user access permissions
                pdf.OwnerPassword = "owner";

                pdf.UserPassword = "user";
                pdf.Permissions.Flags = PdfPermissionFlags.None;

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}