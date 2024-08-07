Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FlattenFormFields
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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
