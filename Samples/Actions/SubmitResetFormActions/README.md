# Use Submit and Reset form actions in PDF documents
This sample shows how to use Submit-Form and Reset-Form actions in PDF documents with the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

A [Submit-Form action](https://api.docotic.com/pdfsubmitformaction) sends the names and values of associated controls to a specified uniform resource identifier (URI). The receiving web server processes the submitted data and returns a response.

A [Reset-Form action](https://api.docotic.com/pdfresetformaction) resets associated controls to their default values.

To associate controls with either action, use the `Add` method of the `PdfSubmitFormAction.Controls` or `PdfResetFormAction.Controls` collection, respectively.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add annotations, actions and scripts to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/annotations-and-actions)