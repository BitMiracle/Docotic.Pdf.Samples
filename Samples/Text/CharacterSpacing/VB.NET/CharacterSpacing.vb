Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CharacterSpacing
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "CharacterSpacing.pdf"

            Using pdf As New PdfDocument()

                Const sampleText As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit"
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.CharacterSpacing = 1
                canvas.DrawString(10, 50, sampleText)

                canvas.CharacterSpacing = 4
                canvas.DrawString(10, 60, sampleText)

                canvas.CharacterSpacing = 10
                canvas.DrawString(10, 70, sampleText)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace