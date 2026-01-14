# Convert XML to PDF using XSLT in C# and VB.NET

This sample shows how to transform XML to PDF by using XSLT to generate HTML as an intermediate step.

## Description

The XML format is machine‑readable, unambiguous, and consistent, but it's not very convenient for humans to read. A common solution is to convert XML into a PDF before presenting an invoice or other structured data to users.

This sample code demonstrates how to transform an invoice in XML format using an XSLT stylesheet to generate clean HTML from the structured data. It then converts that HTML into a final PDF document using Docotic.Pdf together with the [free HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/). This approach leverages XML for structured data, XSLT for flexible presentation logic, and HTML as a reliable intermediate format for rendering.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [HTML to PDF for .NET](https://bitmiracle.com/pdf-library/html-pdf/)
* [Convert HTML to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/html-pdf/convert)
