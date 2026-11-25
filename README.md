# Docotic.Pdf.Samples

Contains C# and VB.NET sample code demonstrating how to use [Docotic.Pdf](https://bitmiracle.com/pdf-library/), a commercial PDF library for .NET.

[All Samples](/Samples)

[Actions](/Samples/Actions)

[Attachments](/Samples/Attachments)

[Compression](/Samples/Compression)

[Digital signatures](/Samples/Digital%20signatures)

[Display options](/Samples/Display%20options)

[Draw and print PDF](/Samples/Draw%20and%20print%20PDF)

[Forms and Annotations](/Samples/Forms%20and%20Annotations)

[General operations](/Samples/General%20operations)

[Graphics](/Samples/Graphics)

[HTML to PDF](/Samples/HtmlToPdf)

[Images](/Samples/Images)

[Layers](/Samples/Layers)

[Layout](/Samples/Layout)

[Metadata](/Samples/Metadata)

[Multithreading](/Samples/Multithreading)

[Pages and Navigation](/Samples/Pages%20and%20Navigation)

[PDF/A](/Samples/PdfA)

[Security](/Samples/Security)

[Text](/Samples/Text)

[XObjects, watermarks and backgrounds](/Samples/XObjects%2C%20watermarks%20and%20backgrounds)

## Docotic.Pdf library

[Docotic.Pdf](https://bitmiracle.com/pdf-library/) is a PDF library for .NET 10 through .NET 5, .NET Standard, .NET Core, and .NET Framework 4.x.

Docotic.Pdf requires a license, which can be [purchased](https://bitmiracle.com/pdf-library/licenses/) or obtained [free of charge](https://bitmiracle.com/pdf-library/licenses/free) in certain cases.

Request a free time limited license on [the library page](https://bitmiracle.com/pdf-library/).

The latest version is available from the [download page](https://bitmiracle.com/pdf-library/download) and [NuGet](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf/).

The following add-ons are available free of charge:
* [HTML to PDF](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/)
* [Layout](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/)
* [Conformance](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Conformance/)
* [Logging](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Logging/)
* [Gdi](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Gdi/)

[Docotic.Pdf Documentation](https://bitmiracle.com/pdf-library/help).

## Running the samples

To run the samples, you must have a valid Docotic.Pdf license.

Each sample that uses Docotic.Pdf must be configured with a valid license key. Replace `PUT-LICENSE-HERE` in the following code with your license key:

```csharp
LicenseManager.AddLicenseData("PUT-LICENSE-HERE");
```

Without a valid license, Docotic.Pdf will not allow you to create or read PDF documents.

## Sample Code license

The sample code in this repository is licensed under the MIT License. You are free to use, modify, and redistribute the samples, including in public or private repositories and commercial applications.

This license applies only to the sample code. Docotic.Pdf is a separate commercial library licensed under its own [EULA](https://bitmiracle.com/pdf-library/licenses/eula).

See the [LICENSE](LICENSE.md) file for details.
