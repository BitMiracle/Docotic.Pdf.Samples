using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace XmlToPdfUsingXslt;

class XmlToPdfUsingXslt
{
    static async Task Main()
    {
        // NOTE:
        // Without a license, the library won't allow you to create or read PDF documents.
        // To get a free time-limited license key, use the form on
        // https://bitmiracle.com/pdf-library/download

        LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

        var transform = CreateTransform(@"..\Sample Data\invoice.xsl");
        var html = TransformToHtml(@"..\Sample Data\invoice.xml", transform);

        var outputFileName = "XmlToPdfUsingXslt.pdf";
        using (var converter = await HtmlConverter.CreateAsync())
        {
            // If the converted HTML uses relative paths for images, CSS files, or other resources,
            // don't forget to specify the base URL.
            using var pdf = await converter.CreatePdfFromStringAsync(html);
            pdf.Save(outputFileName);
        }

        Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

        Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
    }

    private static XslCompiledTransform CreateTransform(string xsltFileName)
    {
        var transform = new XslCompiledTransform();
        transform.Load(xsltFileName);
        return transform;
    }

    private static string TransformToHtml(string xmlFileName, XslCompiledTransform transform)
    {
        using var xmlReader = XmlReader.Create(xmlFileName);
        using var sw = new StringWriter();
        using var writer = XmlWriter.Create(sw, transform.OutputSettings);
        transform.Transform(xmlReader, null, writer);
        return sw.ToString();
    }
}
