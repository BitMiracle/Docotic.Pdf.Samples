Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class InitialView
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            UseFitHeight()
            UsePercentZoom()

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub

        Private Shared Sub UseFitHeight()
            Using pdf As New PdfDocument()

                pdf.InitialView = pdf.CreateView(0)
                pdf.InitialView.SetFitHeight()

                Dim pathToFile As String = "FitHeight.pdf"
                pdf.Save(pathToFile)

                Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
            End Using
        End Sub

        Private Shared Sub UsePercentZoom()
            Using pdf As New PdfDocument("..\Sample Data\form.pdf")

                pdf.InitialView = pdf.CreateView(0)
                pdf.InitialView.SetZoom(40)

                Dim pathToFile As String = "Percent.pdf"
                pdf.Save(pathToFile)

                Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
            End Using
        End Sub
    End Class
End Namespace