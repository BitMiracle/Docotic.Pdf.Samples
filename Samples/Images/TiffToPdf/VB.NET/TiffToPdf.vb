Namespace BitMiracle.Docotic.Pdf.Samples
    Public Class TiffToPdf
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.
            Dim pdf As PdfDocument = New PdfDocument
            Dim tiffFiles() As String = New String() {"Sample Data\multipage.tif", "Sample Data\pinkMask.tif"}
            Dim imagesAdded As Integer = 0
            For Each tiff As String In tiffFiles
                ' open potentially multipage TIFF
                Dim frames As PdfImageFrames = pdf.OpenImage(tiff)
                For Each frame As PdfImageFrame In frames
                    If (imagesAdded <> 0) Then
                        pdf.AddPage()
                    End If

                    Dim image As PdfImage = pdf.AddImage(frame)
                    Dim pdfPage As PdfPage = pdf.Pages((pdf.PageCount - 1))
                    ' adjust page size, so image fill occupy whole page
                    pdfPage.Height = image.Height
                    pdfPage.Width = image.Width
                    pdfPage.Canvas.DrawImage(image, 0, 0, pdfPage.Width, pdfPage.Height, 0)
                    imagesAdded = (imagesAdded + 1)
                Next
            Next
            pdf.Save("TiffToPdf.pdf")
            Process.Start("TiffToPdf.pdf")
        End Sub
    End Class
End Namespace