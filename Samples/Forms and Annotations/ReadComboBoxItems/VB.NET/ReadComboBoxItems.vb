Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ReadComboBoxItems
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument("Sample data/ComboBoxes.pdf")

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
                    MessageBox.Show("No combo boxes found on first page")
                Else
                    MessageBox.Show(sb.ToString())
                End If

            End Using
        End Sub
    End Class
End Namespace
