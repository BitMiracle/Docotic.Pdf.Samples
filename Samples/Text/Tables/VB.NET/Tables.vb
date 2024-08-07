Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Tables
        Private Const TableWidth As Single = 400.0F
        Private Const RowHeight As Single = 30.0F
        Private Shared ReadOnly m_leftTableCorner As New PdfPoint(10, 50)
        Private Shared ReadOnly m_columnWidths As Single() = New Single(2) {100, 100, 200}

        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Tables.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                DrawHeader(canvas)
                DrawTableBody(canvas)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub DrawHeader(canvas As PdfCanvas)
            canvas.SaveState()
            canvas.Brush.Color = New PdfGrayColor(75)
            Dim headerBounds As New PdfRectangle(m_leftTableCorner, New PdfSize(TableWidth, RowHeight))
            canvas.DrawRectangle(headerBounds, PdfDrawMode.FillAndStroke)

            Dim cellBounds As PdfRectangle() = New PdfRectangle(2) {
                New PdfRectangle(m_leftTableCorner.X, m_leftTableCorner.Y, m_columnWidths(0), RowHeight),
                New PdfRectangle(m_leftTableCorner.X + m_columnWidths(0), m_leftTableCorner.Y, m_columnWidths(1), RowHeight),
                New PdfRectangle(m_leftTableCorner.X + m_columnWidths(0) + m_columnWidths(1), m_leftTableCorner.Y, m_columnWidths(2), RowHeight)}

            For i As Integer = 1 To 2
                canvas.CurrentPosition = New PdfPoint(cellBounds(i).Left, cellBounds(i).Top)
                canvas.DrawLineTo(canvas.CurrentPosition.X, canvas.CurrentPosition.Y + RowHeight)
            Next

            canvas.Brush.Color = New PdfGrayColor(0)
            DrawCenteredText(canvas, "Project", cellBounds(0))
            DrawCenteredText(canvas, "License", cellBounds(1))
            DrawCenteredText(canvas, "Description", cellBounds(2))

            canvas.RestoreState()
        End Sub

        Private Shared Sub DrawTableBody(canvas As PdfCanvas)
            Dim bodyLeftCorner As New PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight)
            Dim firstCellBounds As New PdfRectangle(bodyLeftCorner, New PdfSize(m_columnWidths(0), RowHeight * 2))
            canvas.DrawRectangle(firstCellBounds)
            DrawCenteredText(canvas, "Docotic.Pdf", firstCellBounds)

            Dim cells As PdfRectangle(,) = New PdfRectangle(1, 1) {
                {
                    New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0), bodyLeftCorner.Y, m_columnWidths(1), RowHeight),
                    New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0) + m_columnWidths(1), bodyLeftCorner.Y, m_columnWidths(2), RowHeight)
                },
                {
                    New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0), bodyLeftCorner.Y + RowHeight, m_columnWidths(1), RowHeight),
                    New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0) + m_columnWidths(1), bodyLeftCorner.Y + RowHeight, m_columnWidths(2), RowHeight)
                }
            }

            For Each rect As PdfRectangle In cells
                canvas.DrawRectangle(rect, PdfDrawMode.Stroke)
            Next

            DrawCenteredText(canvas, "Application License", cells(0, 0))
            DrawCenteredText(canvas, "For end-user applications", cells(0, 1))
            DrawCenteredText(canvas, "Server License", cells(1, 0))
            DrawCenteredText(canvas, "For server-based services", cells(1, 1))
        End Sub

        Private Shared Sub DrawCenteredText(canvas As PdfCanvas, text As String, bounds As PdfRectangle)
            Dim options = New PdfTextDrawingOptions(bounds) With
            {
                .HorizontalAlignment = PdfTextAlign.Center,
                .VerticalAlignment = PdfVerticalAlign.Center
            }
            canvas.DrawText(text, options)
        End Sub
    End Class
End Namespace