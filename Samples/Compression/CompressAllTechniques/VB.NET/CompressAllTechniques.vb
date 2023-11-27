Imports System.IO
Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CompressAllTechniques
        <STAThread>
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim originalFile As String = "..\Sample Data\jpeg.pdf"
            Const compressedFile As String = "CompressAllTechniques.pdf"

            Using pdf As New PdfDocument(originalFile)
                ' 1. Remove duplicate objects such as fonts and images
                pdf.ReplaceDuplicateObjects()

                ' 2. Recompress images
                Dim alreadyCompressedImageIds = New HashSet(Of String)()
                For i As Integer = 0 To pdf.PageCount - 1
                    Dim page As PdfPage = pdf.Pages(i)

                    For Each painted As PdfPaintedImage In page.GetPaintedImages()
                        Dim image As PdfImage = painted.Image

                        ' Image ID may be null for inline images, which cannot be recompressed
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

                ' 3. Setup save options
                Dim saveOptions As New PdfSaveOptions With
                {
                    .Compression = PdfCompression.Flate,
                    .UseObjectStreams = True,
                    .RemoveUnusedObjects = True,
                    .OptimizeIndirectObjects = True,
                    .WriteWithoutFormatting = True
                }

                ' 4. Remove structure information
                pdf.RemoveStructureInformation()

                ' 5. Flatten form fields 
                ' Controls become uneditable after that. Alternatively, you can use PdfDocument.FlattenWidgets() method
                ' to flatten controls and annotations.
                pdf.FlattenControls()

                ' 6. Clear metadata
                pdf.Metadata.Unembed()
                pdf.Info.Clear(False)

                ' 7. Unembed fonts
                UnembedFonts(pdf)

                ' 8. Remove unused resources
                pdf.RemoveUnusedResources()

                ' 9. Remove unused font glyphs
                pdf.RemoveUnusedFontGlyphs()

                ' 10. Remove page-piece dictionaries
                pdf.RemovePieceInfo()

                ' 11. Merge page content streams
                For Each page As PdfPage In pdf.Pages
                    page.MergeContentStreams()
                Next

                pdf.Save(compressedFile, saveOptions)
            End Using

            Dim message As String = String.Format(
                "Original file size: {0} bytes;" & vbCrLf & "Compressed file size: {1} bytes",
                New FileInfo(originalFile).Length,
                New FileInfo(compressedFile).Length
            )
            Console.WriteLine(message)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(compressedFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function OptimizeImage(painted As PdfPaintedImage) As Boolean
            Dim image As PdfImage = painted.Image

            ' inline images cannot be recompressed unless you move them to resources
            ' using PdfCanvas.MoveInlineImagesToResources 
            If image.IsInline Then
                Return False
            End If

            ' mask images are not good candidates for recompression
            If image.IsMask OrElse image.Width < 8 OrElse image.Height < 8 Then
                Return False
            End If

            ' get size of the painted image
            Dim width As Integer = Math.Max(1, Int(painted.Bounds.Width))
            Dim height As Integer = Math.Max(1, Int(painted.Bounds.Height))

            ' calculate resize ratio
            Dim ratio As Double = Math.Min(image.Width / CDbl(width), image.Height / CDbl(height))

            If ratio <= 1 Then
                ' the image size is smaller then the painted size
                Return RecompressImage(image)
            End If

            If ratio < 1.1 Then
                ' with ratio this small, the potential size reduction
                ' usually does not justify resizing artefacts
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
                    ' JPEG better preserves detail on smaller images 
                    image.RecompressWithJpeg()
                Else
                    ' you can try larger compressio ratio for bigger images
                    image.RecompressWithJpeg2000(10)
                End If

                Return True
            End If

            Return False
        End Function

        Private Shared Function ResizeBilevelImage(image As PdfImage, ratio As Double) As Boolean
            ' Fax documents usually look better if integer-ratio scaling is used
            ' Fractional-ratio scaling introduces more artifacts
            Dim intRatio As Integer = ratio

            ' decrease the ratio when it is too high
            If intRatio > 3 Then
                intRatio = Math.Min(intRatio - 2, 3)
            End If

            If intRatio = 1 AndAlso image.Compression = PdfImageCompression.Group4Fax Then
                ' skipping the image, because the output size and compression are the same
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

        ''' <summary>
        ''' This method unembeds any font that is:
        ''' * installed in the OS
        ''' * or has its name included in the "always unembed" list
        ''' * and its name is not included in the "always keep" list. 
        ''' </summary>
        Private Shared Sub UnembedFonts(pdf As PdfDocument)
            Dim alwaysUnembedList = {"MyriadPro-Regular"}
            Dim alwaysKeepList = {"ImportantFontName", "AnotherImportantFontName"}

            For Each font In pdf.GetFonts()

                If Array.Exists(alwaysKeepList, Function(name) Equals(font.Name, name)) Then
                    Continue For
                End If

                If CanUnembed(font) Then
                    font.Unembed()
                    Continue For
                End If

                If Array.Exists(alwaysUnembedList, Function(name) Equals(font.Name, name)) Then
                    font.Unembed()
                End If
            Next
        End Sub

        Private Shared Function CanUnembed(font As PdfFont) As Boolean
            If Not font.Embedded OrElse font.EncodingName = "Built-In" Then
                Return False
            End If

            Dim format As PdfFontFormat = font.Format
            If format = PdfFontFormat.TrueType OrElse format = PdfFontFormat.CidType2 Then
                Return font.IsAvailableThroughLoader()
            End If

            Return font.IsBuiltIn()
        End Function
    End Class
End Namespace
