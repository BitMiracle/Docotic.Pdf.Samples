Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms

Imports Microsoft.VisualBasic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OptimizeImages
        <STAThread>
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const originalFile As String = "Sample Data\jpeg.pdf"
            Const compressedFile As String = "OptimizeImages.pdf"

            Using pdf As New PdfDocument(originalFile)
                Dim alreadyCompressedImageIds = New HashSet(Of String)()
                For i As Integer = 0 To pdf.PageCount - 1
                    Dim page As PdfPage = pdf.Pages(i)

                    For Each painted As PdfPaintedImage In page.GetPaintedImages()
                        Dim image As PdfImage = painted.Image
                        If alreadyCompressedImageIds.Contains(image.Id) Then
                            Continue For
                        End If

                        If optimizeImage(painted, pdf) Then
                            alreadyCompressedImageIds.Add(image.Id)
                        End If
                    Next
                Next

                pdf.Save(compressedFile)
            End Using

            ' NOTE:
            ' This sample shows only one approach to reduce size of a PDF.
            ' Please check CompressAllTechniques sample code to see more approaches.
            ' https://github.com/BitMiracle/Docotic.Pdf.Samples/tree/master/Samples/Compression/CompressAllTechniques

            Dim message As String = String.Format(
                "Original file size: {0} bytes;" & vbCr & vbLf & "Compressed file size: {1} bytes",
                New FileInfo(originalFile).Length,
                New FileInfo(compressedFile).Length
            )
            MessageBox.Show(message)

            Process.Start(compressedFile)
        End Sub

        Private Shared Function optimizeImage(ByVal painted As PdfPaintedImage, ByVal pdf As PdfDocument) As Boolean
            Dim image As PdfImage = painted.Image

            ' inline images can Not be recompressed unless you move them to resources
            ' using PdfCanvas.MoveInlineImagesToResources 
            If image.IsInline Then
                Return False
            End If

            ' mask images are not good candidates for recompression
            If image.IsMask OrElse image.Width < 8 OrElse image.Height < 8 Then
                Return False
            End If

            ' get size of the painted image
            Dim width As Integer = Math.Max(1, CInt(painted.Bounds.Width))
            Dim height As Integer = Math.Max(1, CInt(painted.Bounds.Height))

            If width >= image.Width OrElse height >= image.Height Then
                If image.Mask IsNot Nothing Then
                    Return False
                End If

                If image.ComponentCount = 1 AndAlso image.BitsPerComponent = 1 AndAlso
                    image.Compression <> PdfImageCompression.Group4Fax Then

                    image.RecompressWithGroup4Fax()
                ElseIf image.BitsPerComponent = 8 AndAlso image.ComponentCount >= 3 AndAlso
                    image.Compression <> PdfImageCompression.Jpeg AndAlso
                    image.Compression <> PdfImageCompression.Jpeg2000 Then

                    image.RecompressWithJpeg2000(10)
                    ' or image.RecompressWithJpeg();
                End If

                Return True
            End If

            ' try to replace large masked images
            If image.Mask IsNot Nothing Then
                If image.Width < 300 OrElse image.Height < 300 Then
                    Return False
                End If

                Using stream = New MemoryStream()
                    Const ResizeRatio As Double = 1.4

                    Dim newWidth As Double = width / ResizeRatio
                    Dim newHeight As Double = height / ResizeRatio

                    Dim tempPage As PdfPage = pdf.AddPage()
                    tempPage.Canvas.DrawImage(image, 0, 0, newWidth, newHeight, 0)
                    Dim tempPaintedImage As PdfPaintedImage = tempPage.GetPaintedImages()(0)
                    tempPaintedImage.SaveAsPainted(stream)

                    pdf.RemovePage(pdf.PageCount - 1)

                    image.ReplaceWith(stream)
                    Return True
                End Using
            End If

            If image.Compression = PdfImageCompression.Group4Fax OrElse
                image.Compression = PdfImageCompression.Group3Fax Then
                ' Fax documents usually looks better if integer-ratio scaling is used
                ' Fractional-ratio scaling introduces more artifacts
                Dim ratio As Integer = Math.Min(image.Width / width, image.Height / height)

                ' decrease the ratio when it is too high
                If ratio > 6 Then
                    ratio -= 5
                ElseIf ratio > 3 Then
                    ratio -= 2
                End If

                image.ResizeTo(image.Width / ratio, image.Height / ratio, PdfImageCompression.Group4Fax)
            ElseIf image.ComponentCount >= 3 AndAlso image.BitsPerComponent = 8 Then
                image.ResizeTo(width, height, PdfImageCompression.Jpeg, 90)
            Else
                image.ResizeTo(width, height, PdfImageCompression.Flate, 9)
            End If

            Return True
        End Function
    End Class
End Namespace
