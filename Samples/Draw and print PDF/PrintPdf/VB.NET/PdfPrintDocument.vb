Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.Gdi

Namespace BitMiracle.Docotic.Pdf.Samples
    Friend Class PdfPrintDocument
        Implements IDisposable

        Private ReadOnly m_printDocument As PrintDocument
        Private ReadOnly m_printSize As PrintSize

        Private ReadOnly m_pdf As PdfDocument

        Private m_printAction As PrintAction
        Private m_pageIndex As Integer
        Private m_lastPageIndex As Integer
        Private m_printableAreaInPoints As RectangleF

        Public Sub New(pdf As PdfDocument, printSize As PrintSize)
            m_pdf = pdf
            m_printSize = printSize
            m_printDocument = New PrintDocument()
            AddHandler m_printDocument.BeginPrint, AddressOf PrintDocument_BeginPrint
            AddHandler m_printDocument.QueryPageSettings, AddressOf PrintDocument_QueryPageSettings
            AddHandler m_printDocument.PrintPage, AddressOf PrintDocument_PrintPage
            AddHandler m_printDocument.EndPrint, AddressOf PrintDocument_EndPrint
        End Sub

        Public ReadOnly Property PrintDocument As PrintDocument
            Get
                Return m_printDocument
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            m_printDocument.Dispose()
        End Sub

        Public Sub Print(settings As PrinterSettings)
            m_printDocument.PrinterSettings = settings
            m_printDocument.Print()
        End Sub

        Private Sub PrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)
            Dim printDocument = CType(sender, PrintDocument)
            printDocument.OriginAtMargins = False

            m_printAction = e.PrintAction

            Select Case printDocument.PrinterSettings.PrintRange
                Case PrintRange.Selection, PrintRange.CurrentPage
                    m_pageIndex = 0
                    m_lastPageIndex = 0
                    Exit Select
                Case PrintRange.SomePages
                    m_pageIndex = Math.Max(0, printDocument.PrinterSettings.FromPage - 1)
                    m_lastPageIndex = Math.Min(m_pdf.PageCount - 1, printDocument.PrinterSettings.ToPage - 1)
                    Exit Select
                Case Else
                    m_pageIndex = 0
                    m_lastPageIndex = m_pdf.PageCount - 1
                    Exit Select
            End Select
        End Sub

        Private Sub PrintDocument_QueryPageSettings(sender As Object, e As QueryPageSettingsEventArgs)
            Dim page = m_pdf.Pages(m_pageIndex)

            ' Auto-detect portrait/landscape orientation.
            ' Printer settings for orientation are ignored in this sample.
            Dim pageSize = GetPageSizeInPoints(page)
            e.PageSettings.Landscape = pageSize.Width > pageSize.Height

            m_printableAreaInPoints = GetPrintableAreaInPoints(e.PageSettings)
        End Sub

        Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
            Dim gr = e.Graphics
            If gr Is Nothing Then Return

            ' Use points to have consistent units for all contexts
            ' 1. Printer
            ' 2. Print preview
            ' 3. PDF
            gr.PageUnit = GraphicsUnit.Point

            If m_printAction = PrintAction.PrintToPreview Then
                gr.SmoothingMode = SmoothingMode.HighQuality

                gr.Clear(Color.LightGray)
                gr.FillRectangle(Brushes.White, m_printableAreaInPoints)
                gr.IntersectClip(m_printableAreaInPoints)

                gr.TranslateTransform(m_printableAreaInPoints.X, m_printableAreaInPoints.Y)
            End If

            Dim page = m_pdf.Pages(m_pageIndex)
            Dim pageSizeInPoints = GetPageSizeInPoints(page)

            If m_printSize = PrintSize.FitPage Then
                Dim sx As Single = CSng(m_printableAreaInPoints.Width / pageSizeInPoints.Width)
                Dim sy As Single = CSng(m_printableAreaInPoints.Height / pageSizeInPoints.Height)
                Dim scaleFactor = Math.Min(sx, sy)
                CenterContentInPrintableArea(gr, pageSizeInPoints, scaleFactor)
                gr.ScaleTransform(scaleFactor, scaleFactor)
            ElseIf m_printSize = PrintSize.ActualSize Then
                CenterContentInPrintableArea(gr, pageSizeInPoints, 1)
            End If

            page.Draw(gr)
            m_pageIndex += 1
            e.HasMorePages = m_pageIndex <= m_lastPageIndex
        End Sub

        Private Sub PrintDocument_EndPrint(sender As Object, e As PrintEventArgs)
        End Sub

        Private Sub CenterContentInPrintableArea(gr As Graphics, contentSizeInPoints As PdfSize, scaleFactor As Single)
            Dim xDiff As Single = CSng(m_printableAreaInPoints.Width - contentSizeInPoints.Width * scaleFactor)
            Dim yDiff As Single = CSng(m_printableAreaInPoints.Height - contentSizeInPoints.Height * scaleFactor)
            If Math.Abs(xDiff) > 0 OrElse Math.Abs(yDiff) > 0 Then gr.TranslateTransform(xDiff / 2, yDiff / 2)
        End Sub

        Private Shared Function GetPageBox(page As PdfPage) As PdfBox
            ' Copying the Adobe Reader behavior: prefer CropBox, but use MediaBox bounds
            ' when some CropBox bound is out of the MediaBox area.
            Dim mediaBox = page.MediaBox
            Dim cropBox = page.CropBox

            Dim left = cropBox.Left
            Dim bottom = cropBox.Bottom
            Dim right = cropBox.Right
            Dim top = cropBox.Top
            If left < mediaBox.Left OrElse left > mediaBox.Right Then left = mediaBox.Left

            If bottom < mediaBox.Bottom OrElse bottom > mediaBox.Top Then bottom = mediaBox.Bottom

            If right > mediaBox.Right OrElse right < mediaBox.Left Then right = mediaBox.Right

            If top > mediaBox.Top OrElse top < mediaBox.Bottom Then top = mediaBox.Top

            Return New PdfBox(left, bottom, right, top)
        End Function

        Private Shared Function GetPageSizeInPoints(page As PdfPage) As PdfSize
            Dim pageArea = GetPageBox(page)
            Dim userUnit = page.UserUnit
            If page.Rotation = PdfRotation.Rotate90 OrElse page.Rotation = PdfRotation.Rotate270 Then
                Return New PdfSize(pageArea.Height * userUnit, pageArea.Width * userUnit)
            End If

            Return New PdfSize(pageArea.Width * userUnit, pageArea.Height * userUnit)
        End Function

        Private Shared Function GetPrintableAreaInPoints(pageSettings As PageSettings) As RectangleF
            Dim printableArea = pageSettings.PrintableArea

            If pageSettings.Landscape Then
                Dim tmp = printableArea.Width
                printableArea.Width = printableArea.Height
                printableArea.Height = tmp
            End If

            ' PrintableArea is expressed in hundredths of an inch
            Const PrinterSpaceToPoint = 72.0F / 100.0F
            Return New RectangleF(
                printableArea.X * PrinterSpaceToPoint,
                printableArea.Y * PrinterSpaceToPoint,
                printableArea.Width * PrinterSpaceToPoint,
                printableArea.Height * PrinterSpaceToPoint)
        End Function
    End Class
End Namespace
