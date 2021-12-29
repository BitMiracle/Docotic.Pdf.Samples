# Linearize PDF document
This sample shows how to save linearized PDF documents. Such documents are also known as optimized for Fast Web View.

Use the PdfSaveOptions.Linearize property to create a linearized PDF. Adobe Acrobat shows "Yes" in the "Fast Web View" field of the "Document Properties" dialog for linearized files.

A Linearized PDF file is a file that has been organized in a special way to enable efficient incremental access in a network environment.

The primary goal of the linearization is to achieve the following behavior:
- When a document is opened, display the first page as quickly as possible.
- When the user requests another page of an open document, display that page as quickly as possible.
- When data for a page is delivered over a slow channel, display the page incrementally as it arrives.
- Permit user interaction, such as following a link, to be performed even before the entire page has been received and displayed.