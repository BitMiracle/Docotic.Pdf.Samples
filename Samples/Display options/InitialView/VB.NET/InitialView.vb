Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class InitialView
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            useFitHeight()
            usePercentZoom()

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub

        Private Shared Sub useFitHeight()
            Using pdf As New PdfDocument()

                pdf.InitialView = pdf.CreateView(0)
                pdf.InitialView.SetFitHeight()

                pdf.Save("FitHeight.pdf")
            End Using
        End Sub

        Private Shared Sub usePercentZoom()
            Using pdf As New PdfDocument("..\Sample Data\form.pdf")

                pdf.InitialView = pdf.CreateView(0)
                pdf.InitialView.SetZoom(40)

                pdf.Save("Percent.pdf")
            End Using
        End Sub
    End Class
End Namespace