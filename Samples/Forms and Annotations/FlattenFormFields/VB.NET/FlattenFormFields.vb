Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FlattenFormFields
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const pathToFile As String = "FlattenFormFields.pdf"

            Dim flattenWidgets = False
            Dim flattenSomeOfTheControls = False

            Using pdf As New PdfDocument("..\Sample Data\form.pdf")
                pdf.FlattenControls()

                ' Alternatively, you can

                If flattenWidgets Then
                    ' 1. Flatten both controls and annotations
                    pdf.FlattenWidgets()
                End If

                If flattenSomeOfTheControls Then
                    ' 2. Flatten individual controls
                    For Each control As PdfControl In pdf.GetControls().ToArray
                        control.Flatten()
                    Next
                End If

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
