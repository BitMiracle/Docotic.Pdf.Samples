Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FlattenFormFields
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Const pathToFile As String = "FlattenFormFields.pdf"

            Using pdf As New PdfDocument("..\Sample Data\form.pdf")
                pdf.FlattenControls()

                ' Alternatively, you can
                ' 1. Flatten controls and annotations
                ' pdf.FlattenWidgets();
                '
                ' 2. Flatten individual controls
                ' foreach (PdfControl control in pdf.GetControls())
                '     control.Flatten();

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
