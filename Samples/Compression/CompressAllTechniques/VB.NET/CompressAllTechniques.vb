Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CompressAllTechniques
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const originalFile As String = "Sample Data\jpeg.pdf"
            Const compressedFile As String = "CompressAllTechniques.pdf"

            Using pdf As New PdfDocument(originalFile)
                ' 1. Recompress images
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

                ' 2. Setup save options
                pdf.SaveOptions.Compression = PdfCompression.Flate
                pdf.SaveOptions.UseObjectStreams = True
                pdf.SaveOptions.RemoveUnusedObjects = True
                pdf.SaveOptions.OptimizeIndirectObjects = True
                pdf.SaveOptions.WriteWithoutFormatting = True

                ' 3. Remove structure information
                pdf.RemoveStructureInformation()

                ' 4. Flatten form fields 
                ' Controls become uneditable after that
                pdf.FlattenControls()

                ' 5. Clear metadata
                pdf.Metadata.Basic.Clear()
                pdf.Metadata.DublinCore.Clear()
                pdf.Metadata.MediaManagement.Clear()
                pdf.Metadata.Pdf.Clear()
                pdf.Metadata.RightsManagement.Clear()
                pdf.Metadata.[Custom].Properties.Clear()

                For Each schema As XmpSchema In pdf.Metadata.Schemas
                    schema.Properties.Clear()
                Next

                pdf.Info.Clear(False)

                ' 6. Remove font duplicates
                pdf.ReplaceDuplicateFonts()

                ' 7. Unembed fonts
                For Each font As PdfFont In pdf.GetFonts()
                    ' Only unembed popular fonts installed in the typical OS. You can extend
                    ' the list of such fonts in the "if" statement below.
                    If font.Name = "Arial" OrElse font.Name = "Verdana" Then
                        font.Unembed()
                    End If
                Next

                ' 8. Remove unused resources
                pdf.RemoveUnusedResources()

                ' 9. Remove page-piece dictionaries
                pdf.RemovePieceInfo()

                pdf.Save(compressedFile)
            End Using

            Dim message As String = String.Format(
                "Original file size: {0} bytes;" & vbCrLf & "Compressed file size: {1} bytes",
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
                If image.HasMask Then
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
            If image.HasMask Then
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
