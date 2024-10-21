# Add table of contents to PDF documents in C# and VB.NET
This sample shows how to add a table of contents to PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Add sections to your PDF document using [LayoutContainer.Section method](https://api.docotic.com/layout/layoutcontainer-section).
In the table of contents, use the [LayoutContainer.SectionLink](https://api.docotic.com/layout/layoutcontainer-sectionlink) or
[TextContainer.SectionLink](https://api.docotic.com/layout/textcontainer-sectionlink) methods to add section links.
You can use the [TextContainer.FirstPageNumber](https://api.docotic.com/layout/textcontainer-firstpagenumber),
[TextContainer.LastPageNumber](https://api.docotic.com/layout/textcontainer-lastpagenumber) and
[TextContainer.PageCount](https://api.docotic.com/layout/textcontainer-pagecount) methods to reference section pages.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf)
* [Generate PDF documents with complex layout](/Samples/Layout/ComplexLayout)