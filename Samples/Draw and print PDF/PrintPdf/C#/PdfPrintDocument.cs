using System;
using System.Drawing.Printing;
using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Samples.PrintPdf
{
    class PdfPrintDocument : IDisposable
    {
        private PrintDocument m_printDocument;
        private PdfDocument m_pdf;

        private int m_pageIndex;
        private int m_lastPageIndex;

        public PdfPrintDocument(PdfDocument pdf)
        {
            if (pdf == null)
                throw new ArgumentNullException("pdf");

            m_pdf = pdf;

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
            if (settings == null)
                throw new ArgumentNullException("settings");

            m_printDocument.PrinterSettings = settings;
            m_printDocument.Print();
        }

        private void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            PrintDocument printDocument = (PrintDocument)sender;
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

            // PaperSize constructor accepts arguments in hundredth of inch
            double scale = (double)(100 / page.Canvas.Resolution);
            PaperSize paperSize = new PaperSize("Custom", (int)(page.Width * scale), (int)(page.Height * scale));
            e.PageSettings.PaperSize = paperSize;

            bool rotated = (page.Rotation == PdfRotation.Rotate270 || page.Rotation == PdfRotation.Rotate90);
            e.PageSettings.Landscape = rotated;
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            m_pdf.Pages[m_pageIndex].Draw(e.Graphics);

            ++m_pageIndex;
            e.HasMorePages = (m_pageIndex <= m_lastPageIndex);
        }

        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
        }
    }
}
