Imports System.IO
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class RowsColumns
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "RowsColumns.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    doc.Pages(
                        Sub(page)
                            page.Margin(50)

                            page.Content().Column(
                                Sub(c)
                                    c.Item().TextStyle(Function(t) t.Parent.FontSize(8)).Row(
                                        Sub(r)
                                            Dim border = Sub(b As Border) b.Thickness(0.001)
                                            r.ConstantItem(200).Border(border).Padding(5).Text(
                                                Sub(t)
                                                    t.Line("First name: John")
                                                    t.Line("Last name: Doe")
                                                    t.Line("Age: 50")
                                                End Sub)

                                            r.AutoItem().Border(border).Padding(5).RotateRight().Text("Ammerland")

                                            r.RelativeItem(1).Border(border).Padding(5).Column(
                                                Sub(innerColumn)
                                                    innerColumn.Item().AlignCenter().Text("Photo")

                                                    Dim imagePath As FileInfo = New FileInfo("..\Sample Data\ammerland.jpg")
                                                    Dim image As Image = doc.Image(imagePath)
                                                    innerColumn.Item().AlignCenter().MaxWidth(100).Image(image)
                                                End Sub)
                                        End Sub)

                                    Dim loremIpsum As String = File.ReadAllText("..\Sample Data\lorem-ipsum.txt")
                                    c.Item().PaddingTop(10).Text(loremIpsum)
                                End Sub)
                        End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
