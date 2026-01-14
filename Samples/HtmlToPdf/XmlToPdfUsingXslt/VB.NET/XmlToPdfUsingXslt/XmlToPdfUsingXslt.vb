Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Module XmlToPdfUsingXslt
    Public Sub Main()
        Task.Run(
            Async Function()
                Await XmlToPdfUsingXslt()
            End Function
        ).GetAwaiter().GetResult()
    End Sub

    Private Async Function XmlToPdfUsingXslt() As Task
        ' NOTE:
        ' Without a license, the library won't allow you to create or read PDF documents.
        ' To get a free time-limited license key, use the form on
        ' https://bitmiracle.com/pdf-library/download

        LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

        Dim transform = CreateTransform("..\Sample Data\invoice.xsl")
        Dim html = TransformToHtml("..\Sample Data\invoice.xml", transform)

        Dim outputFileName = "XmlToPdfUsingXslt.pdf"

        Using converter = Await HtmlConverter.CreateAsync()
            ' If the converted HTML uses relative paths for images, CSS files, or other resources,
            ' don't forget to specify the base URL.
            Using pdf = Await converter.CreatePdfFromStringAsync(html)
                pdf.Save(outputFileName)
            End Using
        End Using

        Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

        Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
    End Function

    Private Function CreateTransform(xsltFileName As String) As XslCompiledTransform
        Dim transform = New XslCompiledTransform()
        transform.Load(xsltFileName)
        Return transform
    End Function

    Private Function TransformToHtml(xmlFileName As String, transform As XslCompiledTransform) As String
        Using reader = XmlReader.Create(xmlFileName)
            Using sw As New StringWriter()
                Using writer = XmlWriter.Create(sw, transform.OutputSettings)
                    transform.Transform(reader, Nothing, writer)
                End Using
                Return sw.ToString()
            End Using
        End Using
    End Function
End Module
