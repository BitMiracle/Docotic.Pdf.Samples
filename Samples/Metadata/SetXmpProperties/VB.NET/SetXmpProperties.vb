Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SetXmpProperties
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "SetXmpProperties.pdf"

            Using pdf As New PdfDocument()
                pdf.Metadata.DublinCore.Creators = New XmpArray(XmpArrayType.Ordered)
                pdf.Metadata.DublinCore.Creators.Values.Add(New XmpString("me"))
                pdf.Metadata.DublinCore.Creators.Values.Add(New XmpString("Docotic.Pdf"))

                pdf.Metadata.DublinCore.Format = New XmpString("application/pdf")

                pdf.Metadata.Pdf.Producer = New XmpString("me too!")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
