using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Tables
    {
        private const double TableWidth = 400.0;
        private const double RowHeight = 30.0;
        private static readonly PdfPoint m_leftTableCorner = new(10, 50);
        private static readonly double[] m_columnWidths = { 100, 100, 200 };

        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "Tables.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                DrawHeader(canvas);
                DrawTableBody(canvas);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void DrawHeader(PdfCanvas canvas)
        {
            canvas.SaveState();
            canvas.Brush.Color = new PdfGrayColor(75);
            var headerBounds = new PdfRectangle(m_leftTableCorner, new PdfSize(TableWidth, RowHeight));
            canvas.DrawRectangle(headerBounds, PdfDrawMode.FillAndStroke);

            var cellBounds = new PdfRectangle[3]
            {
                new(m_leftTableCorner.X, m_leftTableCorner.Y, m_columnWidths[0], RowHeight),
                new(m_leftTableCorner.X + m_columnWidths[0], m_leftTableCorner.Y, m_columnWidths[1], RowHeight),
                new(m_leftTableCorner.X + m_columnWidths[0] + m_columnWidths[1], m_leftTableCorner.Y, m_columnWidths[2], RowHeight)
            };

            for (int i = 1; i <= 2; ++i)
            {
                canvas.CurrentPosition = new PdfPoint(cellBounds[i].Left, cellBounds[i].Top);
                canvas.DrawLineTo(canvas.CurrentPosition.X, canvas.CurrentPosition.Y + RowHeight);
            }

            canvas.Brush.Color = new PdfGrayColor(0);
            DrawCenteredText(canvas, "Project", cellBounds[0]);
            DrawCenteredText(canvas, "License", cellBounds[1]);
            DrawCenteredText(canvas, "Description", cellBounds[2]);

            canvas.RestoreState();
        }

        private static void DrawTableBody(PdfCanvas canvas)
        {
            var bodyLeftCorner = new PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight);
            var firstCellBounds = new PdfRectangle(bodyLeftCorner, new PdfSize(m_columnWidths[0], RowHeight * 2));
            canvas.DrawRectangle(firstCellBounds);
            DrawCenteredText(canvas, "Docotic.Pdf", firstCellBounds);

            var cells = new PdfRectangle[2, 2]
            {
                {
                    new(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y, m_columnWidths[1], RowHeight),
                    new(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y, m_columnWidths[2], RowHeight)
                },
                {
                    new(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y + RowHeight, m_columnWidths[1], RowHeight),
                    new(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y + RowHeight, m_columnWidths[2], RowHeight)
                }
            };

            foreach (PdfRectangle rect in cells)
                canvas.DrawRectangle(rect, PdfDrawMode.Stroke);

            DrawCenteredText(canvas, "Application License", cells[0, 0]);
            DrawCenteredText(canvas, "For end-user applications", cells[0, 1]);
            DrawCenteredText(canvas, "Server License", cells[1, 0]);
            DrawCenteredText(canvas, "For server-based services", cells[1, 1]);
        }

        private static void DrawCenteredText(PdfCanvas canvas, string text, PdfRectangle bounds)
        {
            var options = new PdfTextDrawingOptions(bounds)
            {
                HorizontalAlignment = PdfTextAlign.Center,
                VerticalAlignment = PdfVerticalAlign.Center
            };
            canvas.DrawText(text, options);
        }
    }
}