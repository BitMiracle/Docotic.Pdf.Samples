using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class EditPageContent
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                int pageCount = pdf.PageCount;
                for (int i = 0; i < pageCount; ++i)
                {
                    PdfPage sourcePage = pdf.Pages[0];
                    PdfPage tempPage = pdf.AddPage();

                    // copy and modify page objects to a temporary page
                    var copier = new PageObjectCopier(pdf, null, replaceColor, shouldRemoveText);
                    copier.Copy(sourcePage, tempPage);

                    // clear content of the source page
                    sourcePage.Canvas.Clear();

                    // copy modified objects from the temporary page to the source page
                    tempPage.Rotation = PdfRotation.None;
                    PdfXObject xobj = pdf.CreateXObject(tempPage);
                    PdfBox mediaBox = sourcePage.MediaBox;
                    sourcePage.Canvas.DrawXObject(xobj, mediaBox.Left, -mediaBox.Bottom, xobj.Width, xobj.Height, 0);

                    // remove the temporary page
                    pdf.RemovePage(pageCount);
                }

                pdf.Save("EditPageContent.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        /// <summary>
        /// Replaces orange color with gray.
        /// </summary>
        private static PdfColor replaceColor(PdfColor color)
        {
            if (color is PdfRgbColor rgb)
            {
                if (rgb.R == 255 && rgb.G == 127 && rgb.B == 64)
                    return new PdfGrayColor(70);
            }

            return color;
        }

        /// <summary>
        /// Removes "(Max...)" text on the right.
        /// </summary>
        private static bool shouldRemoveText(PdfTextData text)
        {
            return text.Bounds.X > 200;
        }
    }
}