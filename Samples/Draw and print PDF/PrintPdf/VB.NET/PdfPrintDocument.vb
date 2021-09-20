Imports System
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

        Private m_pdf As PdfDocument
        Private m_printAction As PrintAction
        Private m_pageIndex As Integer
        Private m_lastPageIndex As Integer
        Private m_printableAreaInPoints As RectangleF

        Public Sub New(ByVal pdf As PdfDocument, ByVal printSize As PrintSize)
            If pdf Is Nothing Then Throw New ArgumentNullException("pdf")
            m_pdf = pdf
            m_printSize = printSize
            m_printDocument = New PrintDocument()
            AddHandler m_printDocument.BeginPrint, AddressOf Me.printDocument_BeginPrint
            AddHandler m_printDocument.QueryPageSettings, AddressOf Me.printDocument_QueryPageSettings
            AddHandler m_printDocument.PrintPage, AddressOf Me.printDocument_PrintPage
            AddHandler m_printDocument.EndPrint, AddressOf Me.printDocument_EndPrint
        End Sub

        Public ReadOnly Property PrintDocument As PrintDocument
            Get
                Return m_printDocument
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            m_printDocument.Dispose()
        End Sub

        Public Sub Print(ByVal settings As PrinterSettings)
            If settings Is Nothing Then Throw New ArgumentNullException("settings")
            m_printDocument.PrinterSettings = settings
            m_printDocument.Print()
        End Sub

        Private Sub printDocument_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
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

        Private Sub printDocument_QueryPageSettings(ByVal sender As Object, ByVal e As QueryPageSettingsEventArgs)
            Dim page = m_pdf.Pages(m_pageIndex)

            ' Auto-detect portrait/landscape orientation.
            ' Printer settings for orientation are ignored in this sample.
            Dim pageSize = getPageSizeInPoints(page)
            e.PageSettings.Landscape = pageSize.Width > pageSize.Height

            m_printableAreaInPoints = getPrintableAreaInPoints(e.PageSettings)
        End Sub

        Private Sub printDocument_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
            Dim gr = e.Graphics

            ' Work in points to have consistent units for all contexts
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
            Dim pageSizeInPoints = getPageSizeInPoints(page)

            If m_printSize = PrintSize.FitPage Then
                Dim sx As Single = CSng(m_printableAreaInPoints.Width / pageSizeInPoints.Width)
                Dim sy As Single = CSng(m_printableAreaInPoints.Height / pageSizeInPoints.Height)
                Dim scaleFactor = Math.Min(sx, sy)
                centerContentInPrintableArea(gr, pageSizeInPoints, scaleFactor)
                gr.ScaleTransform(scaleFactor, scaleFactor)
            ElseIf m_printSize = PrintSize.ActualSize Then
                centerContentInPrintableArea(gr, pageSizeInPoints, 1)
            End If

            page.Draw(gr)
            m_pageIndex += 1
            e.HasMorePages = m_pageIndex <= m_lastPageIndex
        End Sub

        Private Sub printDocument_EndPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
        End Sub

        Private Sub centerContentInPrintableArea(ByVal gr As Graphics, ByVal contentSizeInPoints As PdfSize, ByVal scaleFactor As Single)
            Dim xDiff As Single = CSng(m_printableAreaInPoints.Width - contentSizeInPoints.Width * scaleFactor)
            Dim yDiff As Single = CSng(m_printableAreaInPoints.Height - contentSizeInPoints.Height * scaleFactor)
            If Math.Abs(xDiff) > 0 OrElse Math.Abs(yDiff) > 0 Then gr.TranslateTransform(xDiff / 2, yDiff / 2)
        End Sub

        Private Shared Function getPageBox(ByVal page As PdfPage) As PdfBox
            ' Emit Adobe Reader behavior - prefer CropBox, but use MediaBox bounds when
            ' some CropBox bound Is out of MediaBox area.
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

        Private Shared Function getPageSizeInPoints(ByVal page As PdfPage) As PdfSize
            Dim pageArea = getPageBox(page)
            If page.Rotation = PdfRotation.Rotate90 OrElse page.Rotation = PdfRotation.Rotate270 Then
                Return New PdfSize(pageArea.Height, pageArea.Width)
            End If

            Return pageArea.Size
        End Function

        Private Shared Function getPrintableAreaInPoints(ByVal pageSettings As PageSettings) As RectangleF
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
