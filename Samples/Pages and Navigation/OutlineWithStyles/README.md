# Outline (bookmarks) with styles
This sample shows how to build PDF document's outline (also known as "bookmarks") with colored, bold and italic items.

Docotic.Pdf library provides methods and properties to manipulate document's outline. You can set PdfDocument.PageMode to a PdfPageMode.UseOutlines value to instruct a PDF viewer application to show "Bookmarks" panel when your document is opened. 

PdfDocument.OutlineRoot property provides access to document's outline root. Value of this property is a PdfOutlineItem object. All outline items are instances of this class. Please use PdfOutlineItem methods and properties to access or setup outline items.

You can use PdfOutlineItem.AddChild method to add child items to an outline item. Second parameter for AddChild method is an index of page to associate with added child outline item.