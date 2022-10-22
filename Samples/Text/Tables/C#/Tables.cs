using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Tables
    {
        private const double TableWidth = 400.0;
        private const double RowHeight = 30.0;
        private static readonly PdfPoint m_leftTableCorner = new PdfPoint(10, 50);
        private static readonly double[] m_columnWidths = new double[3] { 100, 100, 200 };

        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Tables.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                drawHeader(canvas);
                drawTableBody(canvas);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void drawHeader(PdfCanvas canvas)
        {
            canvas.SaveState();
            canvas.Brush.Color = new PdfGrayColor(75);
            PdfRectangle headerBounds = new PdfRectangle(m_leftTableCorner, new PdfSize(TableWidth, RowHeight));
            canvas.DrawRectangle(headerBounds, PdfDrawMode.FillAndStroke);

            PdfRectangle[] cellBounds = new PdfRectangle[3]
            {
                new PdfRectangle(m_leftTableCorner.X, m_leftTableCorner.Y, m_columnWidths[0], RowHeight),
                new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0], m_leftTableCorner.Y, m_columnWidths[1], RowHeight),
                new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0] + m_columnWidths[1], m_leftTableCorner.Y, m_columnWidths[2], RowHeight)
            };

            for (int i = 1; i <= 2; ++i)
            {
                canvas.CurrentPosition = new PdfPoint(cellBounds[i].Left, cellBounds[i].Top);
                canvas.DrawLineTo(canvas.CurrentPosition.X, canvas.CurrentPosition.Y + RowHeight);
            }

            canvas.Brush.Color = new PdfGrayColor(0);
            drawCenteredText(canvas, "Project", cellBounds[0]);
            drawCenteredText(canvas, "License", cellBounds[1]);
            drawCenteredText(canvas, "Description", cellBounds[2]);

            canvas.RestoreState();
        }

        private static void drawTableBody(PdfCanvas canvas)
        {
            PdfPoint bodyLeftCorner = new PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight);
            PdfRectangle firstCellBounds = new PdfRectangle(bodyLeftCorner, new PdfSize(m_columnWidths[0], RowHeight * 2));
            canvas.DrawRectangle(firstCellBounds);
            drawCenteredText(canvas, "Docotic.Pdf", firstCellBounds);

            PdfRectangle[,] cells = new PdfRectangle[2, 2]
            {
                {
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y, m_columnWidths[1], RowHeight),
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y, m_columnWidths[2], RowHeight)
                },
                {
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y + RowHeight, m_columnWidths[1], RowHeight),
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y + RowHeight, m_columnWidths[2], RowHeight)
                }
            };

            foreach (PdfRectangle rect in cells)
                canvas.DrawRectangle(rect, PdfDrawMode.Stroke);

            drawCenteredText(canvas, "Application License", cells[0, 0]);
            drawCenteredText(canvas, "For end-user applications", cells[0, 1]);
            drawCenteredText(canvas, "Server License", cells[1, 0]);
            drawCenteredText(canvas, "For server-based services", cells[1, 1]);
        }

        private static void drawCenteredText(PdfCanvas canvas, string text, PdfRectangle bounds)
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