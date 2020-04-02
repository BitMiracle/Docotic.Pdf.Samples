Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Tables
        Private Const TableWidth As Single = 400.0F
        Private Const RowHeight As Single = 30.0F
        Private Shared ReadOnly m_leftTableCorner As New PdfPoint(10, 50)
        Private Shared ReadOnly m_columnWidths As Single() = New Single(2) {100, 100, 200}

        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Tables.pdf"

            Using pdf As New PdfDocument()

                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                drawHeader(canvas)
                drawTableBody(canvas)

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub

        Private Shared Sub drawHeader(ByVal canvas As PdfCanvas)
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
            canvas.DrawString("Project", cellBounds(0), PdfTextAlign.Center, PdfVerticalAlign.Center)
            canvas.DrawString("License", cellBounds(1), PdfTextAlign.Center, PdfVerticalAlign.Center)
            canvas.DrawString("Description", cellBounds(2), PdfTextAlign.Center, PdfVerticalAlign.Center)

            canvas.RestoreState()
        End Sub

        Private Shared Sub drawTableBody(ByVal canvas As PdfCanvas)
            Dim bodyLeftCorner As New PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight)
            Dim firstCellBounds As New PdfRectangle(bodyLeftCorner, New PdfSize(m_columnWidths(0), RowHeight * 2))
            canvas.DrawRectangle(firstCellBounds)
            canvas.DrawString("Docotic.Pdf", firstCellBounds, PdfTextAlign.Center, PdfVerticalAlign.Center)

            Dim cells As PdfRectangle(,) = New PdfRectangle(1, 1) {
                {New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0), bodyLeftCorner.Y, m_columnWidths(1), RowHeight),
                New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0) + m_columnWidths(1), bodyLeftCorner.Y, m_columnWidths(2), RowHeight)},
                {New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0), bodyLeftCorner.Y + RowHeight, m_columnWidths(1), RowHeight),
                New PdfRectangle(bodyLeftCorner.X + m_columnWidths(0) + m_columnWidths(1), bodyLeftCorner.Y + RowHeight, m_columnWidths(2), RowHeight)}}

            For Each rect As PdfRectangle In cells
                canvas.DrawRectangle(rect, PdfDrawMode.Stroke)
            Next

            canvas.DrawString("Application License", cells(0, 0), PdfTextAlign.Center, PdfVerticalAlign.Center)
            canvas.DrawString("For end-user applications", cells(0, 1), PdfTextAlign.Center, PdfVerticalAlign.Center)
            canvas.DrawString("Server License", cells(1, 0), PdfTextAlign.Center, PdfVerticalAlign.Center)
            canvas.DrawString("For server-based services", cells(1, 1), PdfTextAlign.Center, PdfVerticalAlign.Center)
        End Sub
    End Class
End Namespace