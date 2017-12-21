# Create and save PDF document
This sample shows how to create and save PDF documents using Docotic.Pdf library.

The most important class in Docotic.Pdf library is PdfDocument. Each newly created PDF document already contains a page, you can access it using PdfDocument.Pages collection or PdfDocument.GetPage method. When document is completed, you can save it to a file or a stream using PdfDocument.Save(..) method.

When you need to open document immediately after saving, you can use System.Diagnostics.Process class and its Start method. Call to Process.Start("your_pdf.pdf") method will open PDF document in a default PDF viewer, if any.