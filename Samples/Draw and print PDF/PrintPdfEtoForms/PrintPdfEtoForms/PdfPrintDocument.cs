using System;
using System.IO;
using BitMiracle.Docotic.Pdf;
using Eto.Drawing;
using Eto.Forms;

namespace BitMiracle.Docotic.Pdf.Samples.PrintPdfEtoForms
{
    class PdfPrintDocument : IDisposable
    {
        private readonly PrintDocument m_printDocument;
        private readonly PdfDocument m_pdf;

        public PdfPrintDocument(PdfDocument pdf)
        {
            m_pdf = pdf ?? throw new ArgumentNullException(nameof(pdf));

            m_printDocument = new PrintDocument();
            m_printDocument.PrintPage += PrintDocument_PrintPage;
        }

        public void Dispose()
        {
            m_printDocument.Dispose();
        }

        public void Print(PrintSettings settings)
        {
            m_printDocument.PrintSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            m_printDocument.PageCount = settings.SelectedPageRange.Length();
            m_printDocument.Print();
        }

        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            Graphics gr = e.Graphics;

            using var stream = new MemoryStream();
            PdfDrawOptions options = PdfDrawOptions.Create();
            options.HorizontalResolution = gr.DPI;
            options.VerticalResolution = gr.DPI;

            PdfPage page = m_pdf.Pages[m_printDocument.PrintSettings.SelectedPageRange.Start - 1 + e.CurrentPage];
            page.Save(stream, options);

            stream.Position = 0;
            using var bitmap = new Bitmap(stream);
            gr.DrawImage(bitmap, 0, 0);
        }
    }
}
