Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ViewerPreferences
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ViewerPreferences.pdf"

            Using pdf As New PdfDocument()

                pdf.ViewerPreferences.CenterWindow = True
                pdf.ViewerPreferences.FitWindow = True
                pdf.ViewerPreferences.HideMenuBar = True
                pdf.ViewerPreferences.HideToolBar = True
                pdf.ViewerPreferences.HideWindowUI = True

                pdf.Info.Title = "Test title"
                pdf.ViewerPreferences.DisplayTitle = True

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace