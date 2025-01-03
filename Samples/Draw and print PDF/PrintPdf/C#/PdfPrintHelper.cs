﻿using System;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class PdfPrintHelper
    {
        public static DialogResult ShowPrintDialog(PdfDocument pdf, PrintSize printSize)
        {
            using var printDialog = new PrintDialog
            {
                AllowSomePages = true,
                AllowCurrentPage = true,
                AllowSelection = true,
            };

            printDialog.PrinterSettings.MinimumPage = 1;
            printDialog.PrinterSettings.MaximumPage = pdf.PageCount;
            printDialog.PrinterSettings.FromPage = printDialog.PrinterSettings.MinimumPage;
            printDialog.PrinterSettings.ToPage = printDialog.PrinterSettings.MaximumPage;

            var result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using var printDocument = new PdfPrintDocument(pdf, printSize);
                printDocument.Print(printDialog.PrinterSettings);
            }

            return result;
        }

        public static DialogResult ShowPrintPreview(PdfDocument pdf, PrintSize printSize)
        {
            using var previewDialog = new PrintPreviewDialog();
            using var printDocument = new PdfPrintDocument(pdf, printSize);
            previewDialog.Document = printDocument.PrintDocument;

            // By default, the print button sends the preview to the default printer
            // The following method replaces the default button with the custom button.
            // The custom button opens print dialog.
            SetupPrintButton(previewDialog, pdf, printSize);

            // Remove the following line if you do not want preview maximized
            previewDialog.WindowState = FormWindowState.Maximized;

            return previewDialog.ShowDialog();
        }

        private static void SetupPrintButton(PrintPreviewDialog previewDialog, PdfDocument pdf, PrintSize printSize)
        {
            var openPrintDialog = new ToolStripButton
            {
                // reuse the image of the default print button
                Image = ((ToolStrip)previewDialog.Controls[1]).ImageList.Images[0],
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };

            openPrintDialog.Click += new EventHandler((sender, e) =>
            {
                if (ShowPrintDialog(pdf, printSize) == DialogResult.OK)
                    previewDialog.Close();
            });

            ((ToolStrip)previewDialog.Controls[1]).Items.RemoveAt(0);
            ((ToolStrip)previewDialog.Controls[1]).Items.Insert(0, openPrintDialog);
        }
    }
}
