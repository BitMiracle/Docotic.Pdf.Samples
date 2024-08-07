Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ReadComboBoxItems
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Using pdf As New PdfDocument("..\Sample data\ComboBoxes.pdf")

                Dim sb As New System.Text.StringBuilder()
                For Each widget As PdfWidget In pdf.Pages(0).Widgets
                    Dim comboBox As PdfComboBox = TryCast(widget, PdfComboBox)
                    If comboBox IsNot Nothing Then
                        sb.Append("Combobox '")
                        sb.Append(comboBox.Name)
                        sb.Append("' contains following items:" & vbLf)

                        For Each item As PdfListItem In comboBox.Items
                            sb.Append(item)
                            sb.Append(vbLf)
                        Next

                        sb.Append(vbLf)
                    End If
                Next

                If sb.Length = 0 Then
                    Console.WriteLine("No combo boxes found on first page")
                Else
                    Console.WriteLine(sb.ToString())
                End If

            End Using
        End Sub
    End Class
End Namespace
