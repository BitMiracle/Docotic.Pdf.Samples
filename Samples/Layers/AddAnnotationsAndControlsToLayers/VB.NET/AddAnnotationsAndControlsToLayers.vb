Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddAnnotationsAndControlsToLayers
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "AddAnnotationsAndControlsToLayers.pdf"

            Using pdf As New PdfDocument()
                pdf.PageMode = PdfPageMode.UseOC

                Dim buttonsLayer As PdfLayer = pdf.CreateLayer("Buttons")
                Dim textLayer As PdfLayer = pdf.CreateLayer("Text")

                Dim page As PdfPage = pdf.Pages(0)

                Dim top As Integer = 100
                Dim height As Integer = 50
                For Each name As String In New String() {"Button 1", "Button 2", "Button 3"}
                    Dim button As PdfButton = page.AddButton(name, New PdfRectangle(10, top, 100, height))
                    button.Text = name
                    button.Layer = buttonsLayer

                    top += height + 10
                Next

                top = 100
                height = 50
                For Each title As String In New String() {"Text 1", "Text 2", "Text 3"}
                    Dim annot As PdfTextAnnotation = page.AddTextAnnotation(New PdfPoint(200, top), title)
                    annot.Layer = textLayer
                    top += height + 10
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
