Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CompressWithSaveOptions
        Public Shared Sub Main()
            Dim originalFile As String = "..\Sample Data\BRAILLE CODES WITH TRANSLATION.pdf"
            Const compressedFile As String = "CompressWithSaveOptions.pdf"

            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument(originalFile)
                Dim saveOptions As New PdfSaveOptions With
                {
                    .Compression = PdfCompression.Flate,
                    .UseObjectStreams = True,
                    .RemoveUnusedObjects = True,
                    .OptimizeIndirectObjects = True,
                    .WriteWithoutFormatting = True
                }
                pdf.Save(compressedFile, saveOptions)
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
            Console.WriteLine(message)
            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace