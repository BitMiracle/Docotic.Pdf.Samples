# Add table of contents to PDF documents in C# and VB.NET
This sample shows how to add table of contents to PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Add sections to your PDF document using [LayoutContainer.Section method](https://bitmiracle.com/pdf-library/help/layoutcontainer.section.html).
In table of contents, use [LayoutContainer.SectionLink](https://bitmiracle.com/pdf-library/help/layoutcontainer.sectionlink.html) or
[TextContainer.SectionLink](https://bitmiracle.com/pdf-library/help/textcontainer.sectionlink.html) methods to add section links.
You can use `TextContainer.FirstPageNumber`, `TextContainer.LastPageNumber` and `TextContainer.PageCount` methods to reference section pages.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf.aspx) article
* [Generate PDF documents with complex layout](/Samples/Layout/ComplexLayout)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)