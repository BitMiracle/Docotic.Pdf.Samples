Imports System.IO
Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OptimizeImages
        <STAThread>
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim originalFile As String = "..\Sample Data\jpeg.pdf"
            Const compressedFile As String = "OptimizeImages.pdf"

            Using pdf As New PdfDocument(originalFile)
                Dim alreadyCompressedImageIds = New HashSet(Of String)()
                For i As Integer = 0 To pdf.Pages.Count - 1
                    Dim page As PdfPage = pdf.Pages(i)

                    For Each painted As PdfPaintedImage In page.GetPaintedImages()
                        Dim image As PdfImage = painted.Image

                        ' Inline images have no ID and cannot be recompressed
                        If image.Id Is Nothing Then
                            Continue For
                        End If

                        If alreadyCompressedImageIds.Contains(image.Id) Then
                            Continue For
                        End If

                        If OptimizeImage(painted) Then
                            alreadyCompressedImageIds.Add(image.Id)
                        End If
                    Next
                Next

                pdf.Save(compressedFile)
            End Using

            ' NOTE:
            ' This sample demonstrates one approach to reducing PDF file size.
            ' See the CompressAllTechniques sample for additional compression techniques
            ' https://github.com/BitMiracle/Docotic.Pdf.Samples/tree/master/Samples/Compression/CompressAllTechniques

            Dim message As String = String.Format(
                "Original file size: {0} bytes;" & vbCr & vbLf & "Compressed file size: {1} bytes",
                New FileInfo(originalFile).Length,
                New FileInfo(compressedFile).Length
            )
            Console.WriteLine(message)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(compressedFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function OptimizeImage(painted As PdfPaintedImage) As Boolean
            Dim image As PdfImage = painted.Image

            ' Inline images cannot be recompressed unless they are first moved to page resources
            ' using PdfCanvas.MoveInlineImagesToResources.
            If image.IsInline Then
                Return False
            End If

            ' Skip mask images and very small images, as they are poor candidates for
            ' recompression.
            If image.IsMask OrElse image.Width < 8 OrElse image.Height < 8 Then
                Return False
            End If

            ' Get the dimensions of the painted image.
            Dim width As Integer = Math.Max(1, Int(painted.Bounds.Width))
            Dim height As Integer = Math.Max(1, Int(painted.Bounds.Height))

            ' Calculate the ratio between the original image size and the painted size.
            Dim ratio As Double = Math.Min(image.Width / CDbl(width), image.Height / CDbl(height))

            If ratio <= 1 Then
                ' The image is no larger than its painted size, so recompress it without resizing.
                Return RecompressImage(image)
            End If

            If ratio < 1.1 Then
                ' The image is only slightly larger than its painted size.
                ' Resizing Is unlikely to reduce the file size enough to justify the quality loss.
                Return False
            End If

            If image.Compression = PdfImageCompression.Group4Fax OrElse
                image.Compression = PdfImageCompression.Group3Fax OrElse
                image.Compression = PdfImageCompression.JBig2 OrElse
                (image.ComponentCount = 1 And image.BitsPerComponent = 1) Then
                Return ResizeBilevelImage(image, ratio)
            End If

            Dim resizedWidth As Integer = Int(Math.Floor(image.Width / ratio))
            Dim resizedHeight As Integer = Int(Math.Floor(image.Height / ratio))

            If (image.ComponentCount >= 3 AndAlso image.BitsPerComponent = 8) OrElse IsGrayJpeg(image) Then
                image.ResizeTo(resizedWidth, resizedHeight, PdfImageCompression.Jpeg, 90)
                ' or image.ResizeTo(resizedWidth, resizedHeight, PdfImageCompression.Jpeg2000, 10);
            Else
                image.ResizeTo(resizedWidth, resizedHeight, PdfImageCompression.Flate, 9)
            End If

            Return True
        End Function

        Private Shared Function RecompressImage(image As PdfImage) As Boolean
            If image.ComponentCount = 1 AndAlso
                image.BitsPerComponent = 1 AndAlso
                image.Compression = PdfImageCompression.Group3Fax Then
                image.RecompressWithGroup4Fax()
                Return True
            End If

            If image.BitsPerComponent = 8 AndAlso
                image.ComponentCount >= 3 AndAlso
                image.Compression <> PdfImageCompression.Jpeg AndAlso
                image.Compression <> PdfImageCompression.Jpeg2000 Then
                If image.Width < 64 AndAlso image.Height < 64 Then
                    ' JPEG preserves fine detail better in smaller images.
                    image.RecompressWithJpeg()
                Else
                    ' Larger images can often tolerate more aggressive compression.
                    image.RecompressWithJpeg2000(10)
                End If

                Return True
            End If

            Return False
        End Function

        Private Shared Function ResizeBilevelImage(image As PdfImage, ratio As Double) As Boolean
            ' Fax documents usually look better when scaled by an integer factor.
            ' Fractional scaling tends to introduce more artifacts.
            Dim intRatio As Integer = ratio

            ' Limit the downscaling factor to avoid excessive quality loss.
            If intRatio > 3 Then
                intRatio = Math.Min(intRatio - 2, 3)
            End If

            If intRatio = 1 AndAlso image.Compression = PdfImageCompression.Group4Fax Then
                ' No resize is needed and the image already uses the target compression.
                Return False
            End If

            image.ResizeTo(image.Width / intRatio, image.Height / intRatio, PdfImageCompression.Group4Fax)
            Return True
        End Function

        Private Shared Function IsGrayJpeg(image As PdfImage) As Boolean
            Dim isJpegCompressed = image.Compression = PdfImageCompression.Jpeg OrElse
                image.Compression = PdfImageCompression.Jpeg2000
            Return isJpegCompressed AndAlso image.ComponentCount = 1 AndAlso image.BitsPerComponent = 8
        End Function
    End Class
End Namespace
