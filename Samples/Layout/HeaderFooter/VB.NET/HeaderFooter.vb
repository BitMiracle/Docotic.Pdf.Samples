Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class HeaderFooter
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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
                            page.Size(PdfPaperSize.A5).MarginHorizontal(50)

                            page.Header().Height(60).AlignRight().AlignBottom().Text("Page header")

                            page.Content().PaddingVertical(10).Column(
                                Sub(column)
                                    Dim columnBg = New PdfGrayColor(95)
                                    column.Header().Background(columnBg).Text(
                                        Sub(t)
                                            t.Span("Column header (")
                                            t.CurrentPageNumber()
                                            t.Span(" / ")
                                            t.PageCount()
                                            t.Span(")")
                                        End Sub)

                                    column.Item().Table(
                                        Sub(Table)
                                            Table.Columns(Sub(c) c.RelativeColumn())

                                            Dim tableBg = New PdfGrayColor(70)
                                            Table.Header(Sub(h) h.Cell().Background(tableBg).Text("Table header"))

                                            Dim loremIpsum As String = File.ReadAllText("../Sample Data/lorem-ipsum.txt")
                                            Table.Cell().Text(loremIpsum)

                                            Table.Footer(Sub(f) f.Cell().Background(tableBg).Text("Table footer"))
                                        End Sub)

                                    column.Footer().Background(columnBg).Text("Column footer")
                                End Sub)

                            page.Footer().Height(30).AlignCenter().Text(
                                Sub(t)
                                    t.CurrentPageNumber().
                                        FontColor(New PdfRgbColor(255, 0, 0)).
                                        Format(Function(n) If(n?.ToRoman(), ""))
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
