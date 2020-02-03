Imports System
Imports System.Drawing.Printing
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Samples.PrintPdf
    Friend Class PdfPrintDocument
        Implements IDisposable

        Private m_printDocument As PrintDocument
        Private m_pdf As PdfDocument
        Private m_pageIndex As Integer
        Private m_lastPageIndex As Integer

        Public Sub New(ByVal pdf As PdfDocument)
            If pdf Is Nothing Then Throw New ArgumentNullException("pdf")
            m_pdf = pdf
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
            Dim scale As Double = (100 / page.Canvas.Resolution)
            Dim paperSize As PaperSize = New PaperSize("Custom", CInt((page.Width * scale)), CInt((page.Height * scale)))
            e.PageSettings.PaperSize = paperSize
            Dim rotated = page.Rotation = PdfRotation.Rotate270 OrElse page.Rotation = PdfRotation.Rotate90
            e.PageSettings.Landscape = rotated
        End Sub

        Private Sub printDocument_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
            m_pdf.Pages(m_pageIndex).Draw(e.Graphics)
            m_pageIndex += 1
            e.HasMorePages = m_pageIndex <= m_lastPageIndex
        End Sub

        Private Sub printDocument_EndPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
        End Sub
    End Class
End Namespace
