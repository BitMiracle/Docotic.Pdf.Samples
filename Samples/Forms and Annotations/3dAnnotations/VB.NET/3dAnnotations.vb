Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ThreeDAnnotations
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "3dAnnotations.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim bounds As PdfRectangle = New PdfRectangle(10, 80, 400, 400)
                Dim annot As Pdf3dAnnotation = page.Add3dAnnotation(bounds, "..\Sample Data\dice.u3d")
                annot.Activation.ActivationMode = PdfRichMediaActivationMode.OnPageOpen

                pdf.Save(PathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace