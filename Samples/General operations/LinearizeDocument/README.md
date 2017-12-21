# Linearize PDF document
This sample shows how to save linearized (or optimized for Fast Web View) PDF documents.

Use PdfDocument.SaveOptions.Linearize property to create a linearized PDF. Adobe Acrobat shows "Yes" in "Fast Web View" field of the "Document Properties" dialog for linearized files.

A Linearized PDF file is a file that has been organized in a special way to enable efficient incremental access in a network environment.
Primary goal of the linearization is to achieve the following behavior:
- When a document is opened, display the first page as quickly as possible.
- When the user requests another page of an open document, display that page as quickly as possible.
- When data for a page is delivered over a slow channel, display the page incrementally as it arrives.
- Permit user interaction, such as following a link, to be performed even before the entire page has been received and displayed.