﻿using System;
using System.Windows;
using System.Windows.Forms;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public partial class MainWindow : Window
    {
        private const int FitPageIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private PrintSize GetPrintSize()
        {
            return (PrintSizeCombobox.SelectedIndex == FitPageIndex) ? PrintSize.FitPage : PrintSize.ActualSize;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            ProcessExistingPdfDocument(PdfPrintHelper.ShowPrintDialog);
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            ProcessExistingPdfDocument(PdfPrintHelper.ShowPrintPreview);
        }

        private void ProcessExistingPdfDocument(Func<PdfDocument, PrintSize, DialogResult> action)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "PDF files (*.pdf)|*.pdf";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // NOTE:
                // When used in trial mode, the library imposes some restrictions.
                // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
                // for more information.

                using var pdf = new PdfDocument(dlg.FileName);
                action(pdf, GetPrintSize());
            }
        }
    }
}
