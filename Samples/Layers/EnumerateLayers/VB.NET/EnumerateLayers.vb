Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class EnumerateLayers
        Private Sub New()
        End Sub
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument("..\Sample Data\BorderPinksOranges.pdf")
                Dim layers As PdfCollection(Of PdfLayer) = pdf.Layers
                For Each layer As PdfLayer In layers
                    Dim message As String = String.Format("Name = {0}" & vbLf & "Visible = {1}" & vbLf & "Intents = ", layer.Name, layer.Visible)

                    For Each intent As PdfLayerIntent In layer.GetIntents()
                        message += intent.ToString()
                        message += " "
                    Next

                    Console.WriteLine("Layer Info:")
                    Console.WriteLine(message)
                    Console.WriteLine()
                Next
            End Using
        End Sub
    End Class
End Namespace
