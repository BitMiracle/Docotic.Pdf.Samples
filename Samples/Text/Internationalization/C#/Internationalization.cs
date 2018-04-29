using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Internationalization
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.FontSize = 12;

                canvas.Font = pdf.AddFont(PdfBuiltInFont.CourierBold);
                canvas.DrawString(10, 50, "Chinese(traditional): ");
                canvas.DrawString(10, 70, "Russian: ");
                canvas.DrawString(10, 90, "Portugal: ");

                canvas.Font = pdf.AddFont("NSimSun");
                canvas.DrawString(180, 50, "世界您好");
                canvas.Font.RemoveUnusedGlyphs();

                canvas.Font = pdf.AddFont("Times New Roman");
                canvas.DrawString(180, 70, "Привет, мир");
                canvas.DrawString(180, 90, "Olá mundo");
                canvas.Font.RemoveUnusedGlyphs();

                pdf.Save("Internationalization.pdf");
            }

            Process.Start("Internationalization.pdf");
        }
    }
}