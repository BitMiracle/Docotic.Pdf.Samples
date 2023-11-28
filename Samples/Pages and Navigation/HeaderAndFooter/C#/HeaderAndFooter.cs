using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class HeaderAndFooter
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            const string PathToFile = "HeaderAndFooter.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\BRAILLE CODES WITH TRANSLATION.pdf"))
            {
                PdfFont font = pdf.AddFont(PdfBuiltInFont.Helvetica);

                for (int i = 0; i < pdf.PageCount; ++i)
                {
                    PdfPage page = pdf.Pages[i];
                    DrawHeader(page, font);
                    DrawFooter(i, page, font);
                }

                pdf.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }

        private static void DrawHeader(PdfPage page, PdfFont font)
        {
            const string HeaderText = "Header";

            var canvas = page.Canvas;
            canvas.Font = font;
            canvas.FontSize = 30;

            double textWidth = canvas.GetTextWidth(HeaderText);
            double rotatedPageWidth = GetRotatedPageSize(page).Width;
            var headerPosition = new PdfPoint((rotatedPageWidth - textWidth) / 2, 10);

            ShowTextAtRotatedPage(HeaderText, headerPosition, page);
        }

        private static void DrawFooter(int pageIndex, PdfPage page, PdfFont font)
        {
            var canvas = page.Canvas;
            canvas.Font = font;
            canvas.FontSize = 14;

            PdfSize rotatedPageSize = GetRotatedPageSize(page);
            var paddingFromCorner = new PdfSize(30, 30);
            var positionRightBottom = new PdfPoint(
                rotatedPageSize.Width - paddingFromCorner.Width,
                rotatedPageSize.Height - paddingFromCorner.Height
            );
            
            ShowTextAtRotatedPage((pageIndex + 1).ToString(), positionRightBottom, page);
        }

        private static PdfSize GetRotatedPageSize(PdfPage page)
        {
            if (page.Rotation == PdfRotation.Rotate90 || page.Rotation == PdfRotation.Rotate270)
                return new PdfSize(page.Height, page.Width);

            return new PdfSize(page.Width, page.Height);
        }

        private static void ShowTextAtRotatedPage(string text, PdfPoint position, PdfPage page)
        {
            PdfCanvas canvas = page.Canvas;
            if (page.Rotation == PdfRotation.None)
            {
                canvas.DrawString(position.X, position.Y, text);
                return;
            }

            canvas.SaveState();

            switch (page.Rotation)
            {
                case PdfRotation.Rotate90:
                    canvas.TranslateTransform(position.Y, page.Height - position.X);
                    break;

                case PdfRotation.Rotate180:
                    canvas.TranslateTransform(page.Width - position.X, page.Height - position.Y);
                    break;

                case PdfRotation.Rotate270:
                    canvas.TranslateTransform(page.Width - position.Y, position.X);
                    break;
            }
            canvas.RotateTransform((int)page.Rotation * 90);
            canvas.DrawString(0, 0, text);

            canvas.RestoreState();
        }
    }
}