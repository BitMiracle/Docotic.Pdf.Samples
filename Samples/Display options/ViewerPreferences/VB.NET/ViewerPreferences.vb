Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ViewerPreferences
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

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
        End Sub
    End Class
End Namespace