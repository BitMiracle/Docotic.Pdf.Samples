using System;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class ValidateData
    {
        private const double TopMargin = 50;   // the height of the top margin of the document
        private const double RightMargin = 50; // the width of the right margin of the document

        private const double FontSize = 10; // font size in the document

        private static readonly string[] Months =
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        public static void Run()
        {
            string pathToFile = "ValidateData.pdf";

            using (var pdf = new PdfDocument())
            {
                var page = pdf.Pages[0];

                SetupFont(
                    page.Canvas,
                    pdf.AddFont("Arial") ?? throw new Exception("Failed to load font."));

                var validateNumeric = File.ReadAllText("../Sample Data/ValidateNumeric.js");
                var action = pdf.CreateJavaScriptAction(validateNumeric);
                AddDateField(page, action);

                var setCurrentDate = File.ReadAllText("../Sample Data/SetCurrentDate.js");
                pdf.OnOpenDocument = pdf.CreateJavaScriptAction(setCurrentDate);

                pdf.Save(pathToFile);
            }

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void SetupFont(PdfCanvas canvas, PdfFont font)
        {
            canvas.Font = font;
            canvas.FontSize = FontSize;
        }

        private static void AddDateField(PdfPage page, PdfJavaScriptAction action)
        {
            page.Canvas.FontSize = FontSize;

            double left = page.Width - RightMargin - 150;
            double height = AddDayField(page, action, left, TopMargin);
            AddMonthField(page, height);
            AddYearField(page, action, height);
        }

        private static double AddDayField(
            PdfPage page, PdfJavaScriptAction action, double left, double top)
        {
            var canvas = page.Canvas;
            canvas.DrawString(left, top, "<< ");

            PdfTextBox dayTextBox = page.AddTextBox(
                "day", canvas.TextPosition, canvas.MeasureText("00"));
            SetupTextBox(dayTextBox, canvas.Font);
            dayTextBox.MaxLength = 2;
            dayTextBox.OnKeyPress = action;

            canvas.TextPosition = new PdfPoint(
                canvas.TextPosition.X + dayTextBox.Width,
                canvas.TextPosition.Y
            );

            canvas.DrawString(" >> ");
            return dayTextBox.Height;
        }

        private static void AddMonthField(PdfPage page, double height)
        {
            var canvas = page.Canvas;
            double width = double.MinValue;
            foreach (string month in Months)
            {
                double monthWidth = canvas.GetTextWidth("   " + month + "   ");
                if (monthWidth > width)
                    width = monthWidth;
            }

            canvas.CurrentPosition = new PdfPoint(canvas.TextPosition.X, canvas.TextPosition.Y + height);
            canvas.DrawLineTo(canvas.TextPosition.X + width, canvas.TextPosition.Y + height);

            PdfTextBox monthTextBox = page.AddTextBox("month", canvas.TextPosition, new PdfSize(width, height));
            SetupTextBox(monthTextBox, canvas.Font);

            canvas.TextPosition = new PdfPoint(canvas.TextPosition.X + width, canvas.TextPosition.Y);
            canvas.DrawString("   ");
        }

        private static void AddYearField(PdfPage page, PdfJavaScriptAction action, double height)
        {
            var canvas = page.Canvas;
            double width = canvas.GetTextWidth("0000");

            canvas.CurrentPosition = new PdfPoint(canvas.TextPosition.X, canvas.TextPosition.Y + height);
            canvas.DrawLineTo(canvas.TextPosition.X + width, canvas.TextPosition.Y + height);

            PdfTextBox yearTextBox = page.AddTextBox("year", canvas.TextPosition, new PdfSize(width, height));
            SetupTextBox(yearTextBox, canvas.Font);
            yearTextBox.MaxLength = 4;
            yearTextBox.OnKeyPress = action;
        }

        private static void SetupTextBox(PdfTextBox textBox, PdfFont font)
        {
            textBox.Font = font;
            textBox.FontSize = FontSize;
            textBox.Border.Width = 0;
            textBox.TextAlign = PdfTextAlign.Center;
        }
    }
}
