Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class HeaderFooter
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "HeaderFooter.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    doc.Typography(
                        Sub(t)
                            t.Header = t.Emphasis.FontSize(16).Underline()
                            t.Footer = t.Plain.FontSize(8)
                        End Sub)

                    doc.Pages(
                        Sub(page)
                            page.Size(PdfPaperSize.A5)
                            page.MarginHorizontal(50)

                            page.Header().Height(60).AlignRight().AlignBottom().Text("Page header")

                            Dim loremIpsum = File.ReadAllText("../Sample Data/lorem-ipsum.txt")
                            page.Content().PaddingVertical(10).Text(loremIpsum)

                            page.Footer().Height(30).AlignCenter().Text(
                                Sub(t)
                                    t.CurrentPageNumber().Format(
                                        Function(n)
                                            Return If(n?.ToRoman(), "")
                                        End Function)
                                End Sub)
                        End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class

    Module RomanNumberFormatter
        Private ReadOnly Digits As Integer() = {1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000}
        Private ReadOnly RomanDigits As String() = {"I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M"}

        <Extension()>
        Function ToRoman(number As Integer) As String
            Dim result = New StringBuilder()

            While number > 0
                For i = Digits.Length - 1 To 0 Step -1
                    If number / Digits(i) >= 1 Then
                        number -= Digits(i)
                        result.Append(RomanDigits(i))
                        Exit For
                    End If
                Next
            End While

            Return result.ToString()
        End Function
    End Module
End Namespace
