Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Metadata
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Metadata.pdf"

            Using pdf As New PdfDocument()
                pdf.Info.Author = "Sample Browser application"
                pdf.Info.Subject = "Document metadata"
                pdf.Info.Title = "Custom title goes here"
                pdf.Info.Keywords = "pdf, Docotic.Pdf"

                Dim page As PdfPage = pdf.Pages(0)
                page.Canvas.DrawString(10, 50, "Check document properties")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace