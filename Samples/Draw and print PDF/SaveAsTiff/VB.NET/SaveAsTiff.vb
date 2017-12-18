Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SaveAsTiff
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim outputPath As String = "SaveAsTiff.tiff"

            Using pdf = New PdfDocument("Sample Data\jfif3.pdf")
                Dim options As PdfDrawOptions = PdfDrawOptions.Create()
                options.BackgroundColor = New PdfRgbColor(255, 255, 255)

                pdf.SaveAsTiff(outputPath, options)
            End Using

            Process.Start(outputPath)
        End Sub
    End Class
End Namespace