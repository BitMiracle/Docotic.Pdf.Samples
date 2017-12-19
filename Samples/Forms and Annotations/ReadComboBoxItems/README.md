# Read combo box items
This sample shows how to read items of combo boxes in a PDF document.

You can access all widgets associated with a page using PdfPage.Widgets property. This method returns readonly collection of PdfWidget objects. 
PdfComboBox class is a child of the PdfWidget class. All combo box items can be retrieved using PdfComboBox.Items property.