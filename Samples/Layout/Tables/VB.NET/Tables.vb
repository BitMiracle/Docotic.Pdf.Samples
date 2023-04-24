Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class Tables
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "Tables.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    doc.Pages(
                        Sub(page)
                            page.TextStyle(Function(t) t.Parent.FontSize(8))
                            page.MarginVertical(50)
                            page.MarginHorizontal(10)

                            page.Content().Width(400).Table(
                                Sub(t)
                                    t.Columns(
                                        Sub(c)
                                            c.RelativeColumn(2)
                                            c.RelativeColumn(2)
                                            c.RelativeColumn(3)
                                        End Sub)

                                    t.Header(
                                        Sub(h)
                                            headerCell(h).Text("Project")
                                            headerCell(h).Text("License")
                                            headerCell(h).Text("Description")
                                        End Sub)

                                    For i As Integer = 0 To Products.Length - 1
                                        Dim alt As Boolean = i Mod 2 = 1
                                        Dim p As Product = Products(i)
                                        bodyCell(t, alt, p.Licenses.Length).Text(p.Name)

                                        For Each l As License In p.Licenses
                                            bodyCell(t, alt).Text(l.Name)
                                            bodyCell(t, alt).Text(l.Description)
                                        Next
                                    Next
                                End Sub)
                        End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function headerCell(t As TableCellContainer) As LayoutContainer
            Return t.
                Cell().
                Background(New PdfGrayColor(75)).
                Border(Function(b) b.Thickness(CellBorderThickness)).
                PaddingVertical(CellVerticalPadding).
                AlignCenter().
                AlignMiddle()
        End Function

        Private Shared Function bodyCell(t As Table, alt As Boolean, Optional rowSpan As Integer = 1) As LayoutContainer
            Return t.
                Cell().
                RowSpan(rowSpan).
                Container(Function(c) If(alt, c.Background(New PdfGrayColor(90)), c)).
                Border(Function(b) b.Thickness(CellBorderThickness)).
                PaddingVertical(CellVerticalPadding).
                AlignCenter().
                AlignMiddle()
        End Function

        Private Const CellVerticalPadding = 10
        Private Const CellBorderThickness = 0.1

        Private Shared ReadOnly Products = New Product() {
            New Product("Docotic.Pdf", New License() {
                New License("Application License", "Desktop or mobile applications"),
                New License("Server License", "Server-based services"),
                New License("Ultimate License", "All your PDF needs"),
                New License("Utlimate+ License", "All your PDF needs + premium support")
            }),
            New Product("Jpeg2000.Net", New License() {New License("Ultimate License", "Any services or applications")}),
            New Product("LibTiff.Net", New License() {New License("Free", "")}),
            New Product("LibJpeg.Net", New License() {New License("Free", "")})
        }
    End Class

    NotInheritable Class Product
        Public Sub New(name As String, licenses As License())
            Me.Name = name
            Me.Licenses = licenses
        End Sub

        Public Name As String
        Public Licenses As License()
    End Class

    NotInheritable Class License
        Public Sub New(name As String, description As String)
            Me.Name = name
            Me.Description = description
        End Sub

        Public Name As String
        Public Description As String
    End Class
End Namespace
