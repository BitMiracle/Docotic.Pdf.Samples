# Add table of contents to PDF documents in C# and VB.NET
This sample shows how to add table of contents to PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Add sections to your PDF document using [LayoutContainer.Section method](https://bitmiracle.com/pdf-library/api/layout/layoutcontainer-section).
In table of contents, use [LayoutContainer.SectionLink](https://bitmiracle.com/pdf-library/api/layout/layoutcontainer-sectionlink) or
[TextContainer.SectionLink](https://bitmiracle.com/pdf-library/api/layout/textcontainer-sectionlink) methods to add section links.
You can use [TextContainer.FirstPageNumber](https://bitmiracle.com/pdf-library/api/layout/textcontainer-firstpagenumber),
[TextContainer.LastPageNumber](https://bitmiracle.com/pdf-library/api/layout/textcontainer-lastpagenumber) and
[TextContainer.PageCount](https://bitmiracle.com/pdf-library/api/layout/textcontainer-pagecount) methods to reference section pages.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf) article
* [Generate PDF documents with complex layout](/Samples/Layout/ComplexLayout)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)