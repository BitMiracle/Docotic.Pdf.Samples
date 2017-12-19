Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RadioButtons
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "RadioButtons.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                page.Canvas.DrawString(10, 50, "Docotic.Pdf library is written using: ")

                Dim radioButton As PdfRadioButton = page.AddRadioButton("languages", 10, 60, 100, 15, "C#")
                radioButton.ExportValue = "csharp"
                radioButton.Checked = True

                Dim radioButton2 As PdfRadioButton = page.AddRadioButton("languages", 10, 80, 100, 15, "Python")
                radioButton2.ExportValue = "python"
                radioButton2.Checked = False

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace