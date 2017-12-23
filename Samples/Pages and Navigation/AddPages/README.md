# Add new pages
This samples shows how to add or insert pages to your PDF documents.

You can access the collection of PDF document pages using PdfDocument.Pages property. Use PdfDocument.AddPage() method to add a new page to the end of the document. Use PdfDocument.InsertPage(int position) method to insert a new page to an arbitrary position in the document. Valid range for position argument is (0..Pages.Count) inclusive.

Call to the InsertPage method with position argument equal to PdfDocument.Pages.Count is the same as call to the PdfDocument.AddPage() method. Both of AddPage and InsertPage methods return the newly created page.