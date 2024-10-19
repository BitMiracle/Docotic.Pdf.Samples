# Convert URL to PDF ignoring SSL errors in C# or VB.NET
This sample shows how to convert a web page to PDF ignoring SSL errors in C# or VB.NET application.

The [Ignore SSL errors](https://bitmiracle.com/pdf-library/html-pdf/#ignore-ssl-errors) section contains additional information.

## Description

Self-signed or otherwise untrusted certificates can cause SSL errors. Revoked and expired certificates can cause the HTML converter to throw exceptions, too. In cases when you still would like to convert those HTML pages, use the [engine options](https://api.docotic.com/htmltopdf/htmlengineoptions) with `IgnoreSslErrors = true` to ignore SSL errors.

This sample code uses free [Docotic.Pdf.HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Convert HTML to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/html-pdf/convert)