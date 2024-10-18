# Linearize PDF document in C# and VB.NET

This sample shows how to create linearized PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/). Such documents are also referred to as optimized for Fast Web View.

## Description

Use the [PdfSaveOptions.Linearize](https://api.docotic.com/pdfsaveoptions-linearize) property to create a linearized PDF. Adobe Acrobat shows "Yes" in the "Fast Web View" field of the "Document Properties" dialog for linearized files.

The organization of a Linearized PDF file enables efficient incremental access in a network environment.

The primary goal of the linearization is to achieve the following behavior:
* When a document is opened, display the first page as quickly as possible.
* When the user requests another page of an open document, display that page as quickly as possible.
* When data for a page is delivered over a slow channel, display the page incrementally as it arrives.
* Permit user interaction, such as following a link, to be performed even before the entire page has been received and displayed.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Save options](https://bitmiracle.com/pdf-library/edit/#save-options)
* [Check if the PDF document was created linearized](https://api.docotic.com/pdfdocument-islinearized)