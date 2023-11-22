using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using BitMiracle.Docotic.Pdf;
using BitMiracle.Docotic.Pdf.Gdi;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class PdfPrintDocument : IDisposable
    {
        private readonly PrintDocument m_printDocument;
        private readonly PrintSize m_printSize;

        private readonly PdfDocument m_pdf;

        private PrintAction m_printAction;
        private int m_pageIndex;
        private int m_lastPageIndex;
        private RectangleF m_printableAreaInPoints;


        public PdfPrintDocument(PdfDocument pdf, PrintSize printSize)
        {
            m_pdf = pdf;
            m_printSize = printSize;

            m_printDocument = new PrintDocument();
            m_printDocument.BeginPrint += printDocument_BeginPrint;
            m_printDocument.QueryPageSettings += printDocument_QueryPageSettings;
            m_printDocument.PrintPage += printDocument_PrintPage;
            m_printDocument.EndPrint += printDocument_EndPrint;
        }

        public PrintDocument PrintDocument
        {
            get { return m_printDocument; }
        }

        public void Dispose()
        {
            m_printDocument.Dispose();
        }

        public void Print(PrinterSettings settings)
        {
            m_printDocument.PrinterSettings = settings;
            m_printDocument.Print();
        }

        private void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            PrintDocument printDocument = (PrintDocument)sender;
            printDocument.OriginAtMargins = false;

            m_printAction = e.PrintAction;

            switch (printDocument.PrinterSettings.PrintRange)
            {
                case PrintRange.Selection:
                case PrintRange.CurrentPage:
                    {
                        m_pageIndex = 0;
                        m_lastPageIndex = 0;
                        break;
                    }

                case PrintRange.SomePages:
                    {
                        m_pageIndex = Math.Max(0, printDocument.PrinterSettings.FromPage - 1);
                        m_lastPageIndex = Math.Min(m_pdf.PageCount - 1, printDocument.PrinterSettings.ToPage - 1);
                        break;
                    }

                case PrintRange.AllPages:
                default:
                    {
                        m_pageIndex = 0;
                        m_lastPageIndex = m_pdf.PageCount - 1;
                        break;
                    }
            }
        }

        private void printDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            PdfPage page = m_pdf.Pages[m_pageIndex];

            // Auto-detect portrait/landscape orientation.
            // Printer settings for orientation are ignored in this sample.
            PdfSize pageSize = getPageSizeInPoints(page);
            e.PageSettings.Landscape = pageSize.Width > pageSize.Height;

            m_printableAreaInPoints = getPrintableAreaInPoints(e.PageSettings);
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var gr = e.Graphics;
            if (gr is null)
                return;

            // Work in points to have consistent units for all contexts:
            // 1. Printer
            // 2. Print preview
            // 3. PDF
            gr.PageUnit = GraphicsUnit.Point;

            if (m_printAction == PrintAction.PrintToPreview)
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;

                gr.Clear(Color.LightGray);
                gr.FillRectangle(Brushes.White, m_printableAreaInPoints);
                gr.IntersectClip(m_printableAreaInPoints);

                gr.TranslateTransform(m_printableAreaInPoints.X, m_printableAreaInPoints.Y);
            }

            PdfPage page = m_pdf.Pages[m_pageIndex];
            PdfSize pageSizeInPoints = getPageSizeInPoints(page);

            if (m_printSize == PrintSize.FitPage)
            {
                float sx = (float)(m_printableAreaInPoints.Width / pageSizeInPoints.Width);
                float sy = (float)(m_printableAreaInPoints.Height / pageSizeInPoints.Height);
                float scaleFactor = Math.Min(sx, sy);

                centerContentInPrintableArea(gr, pageSizeInPoints, scaleFactor);
                gr.ScaleTransform(scaleFactor, scaleFactor);
            }
            else if (m_printSize == PrintSize.ActualSize)
            {
                centerContentInPrintableArea(gr, pageSizeInPoints, 1);
            }

            page.Draw(gr);

            ++m_pageIndex;
            e.HasMorePages = (m_pageIndex <= m_lastPageIndex);
        }

        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
        }

        private void centerContentInPrintableArea(Graphics gr, PdfSize contentSizeInPoints, float scaleFactor)
        {
            float xDiff = (float)(m_printableAreaInPoints.Width - contentSizeInPoints.Width * scaleFactor);
            float yDiff = (float)(m_printableAreaInPoints.Height - contentSizeInPoints.Height * scaleFactor);
            if (Math.Abs(xDiff) > 0 || Math.Abs(yDiff) > 0)
                gr.TranslateTransform(xDiff / 2, yDiff / 2);
        }

        private static PdfBox getPageBox(PdfPage page)
        {
            // Emit Adobe Reader behavior - prefer CropBox, but use MediaBox bounds when
            // some CropBox bound is out of MediaBox area.

            PdfBox mediaBox = page.MediaBox;
            PdfBox cropBox = page.CropBox;
            double left = cropBox.Left;
            double bottom = cropBox.Bottom;
            double right = cropBox.Right;
            double top = cropBox.Top;

            if (left < mediaBox.Left || left > mediaBox.Right)
                left = mediaBox.Left;

            if (bottom < mediaBox.Bottom || bottom > mediaBox.Top)
                bottom = mediaBox.Bottom;

            if (right > mediaBox.Right || right < mediaBox.Left)
                right = mediaBox.Right;

            if (top > mediaBox.Top || top < mediaBox.Bottom)
                top = mediaBox.Top;

            return new PdfBox(left, bottom, right, top);
        }

        private static PdfSize getPageSizeInPoints(PdfPage page)
        {
            PdfBox pageArea = getPageBox(page);
            double userUnit = page.UserUnit;
            if (page.Rotation == PdfRotation.Rotate90 || page.Rotation == PdfRotation.Rotate270)
                return new PdfSize(pageArea.Height * userUnit, pageArea.Width * userUnit);

            return new PdfSize(pageArea.Width * userUnit, pageArea.Height * userUnit);
        }

        private static RectangleF getPrintableAreaInPoints(PageSettings pageSettings)
        {
            RectangleF printableArea = pageSettings.PrintableArea;
            if (pageSettings.Landscape)
            {
                float tmp = printableArea.Width;
                printableArea.Width = printableArea.Height;
                printableArea.Height = tmp;
            }

            // PrintableArea is expressed in hundredths of an inch
            const float PrinterSpaceToPoint = 72.0f / 100.0f;
            return new RectangleF(
                printableArea.X * PrinterSpaceToPoint,
                printableArea.Y * PrinterSpaceToPoint,
                printableArea.Width * PrinterSpaceToPoint,
                printableArea.Height * PrinterSpaceToPoint
            );
        }
    }
}
