Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class EnumerateLayers
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Using pdf As New PdfDocument("..\Sample Data\BorderPinksOranges.pdf")
                For Each layer As PdfLayer In pdf.Layers
                    If layer Is Nothing Then Continue For

                    Dim message As String = String.Format("Name = {0}" & vbLf & "Visible = {1}" & vbLf & "Intents = ", layer.Name, layer.Visible)

                    For Each intent As PdfLayerIntent In layer.GetIntents()
                        message += intent.ToString()
                        message += " "
                    Next

                    Console.WriteLine("Layer Info:")
                    Console.WriteLine(message)
                    Console.WriteLine()
                Next
            End Using
        End Sub
    End Class
End Namespace
