Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RichMediaAnnotations
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Const PathToFile As String = "RichMediaAnnotations.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim bounds As New PdfRectangle(10, 50, 200, 100)
                Dim sound As PdfFileSpecification = pdf.CreateFileAttachment("..\Sample Data\sound.mp3")
                Dim annot As PdfRichMediaAnnotation = page.AddRichMediaAnnotation(bounds, sound, PdfRichMediaContentType.Sound)
                annot.Activation.ActivationMode = PdfRichMediaActivationMode.OnPageOpen

                pdf.Save(PathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace