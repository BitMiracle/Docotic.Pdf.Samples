Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CreatePortfolio
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "CreatePortfolio.pdf"

            Using pdf As New PdfDocument()
                Dim first As PdfFileSpecification = pdf.CreateFileAttachment("..\Sample Data\jpeg.pdf")
                pdf.SharedAttachments.Add(first)

                Dim second As PdfFileSpecification = pdf.CreateFileAttachment("..\Sample Data\gmail-cheat-sheet.pdf")
                pdf.SharedAttachments.Add(second)

                Dim col As PdfCollectionSettings = pdf.CreateCollectionSettings()
                pdf.Collection = col
                col.InitialDocument = "jpeg.pdf"
                col.SetTiledView()

                pdf.Pages(0).Canvas.DrawString("You PDF viewer does not support portfolios")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace