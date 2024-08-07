Imports System.Collections.Concurrent
Imports System.IO
Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class MergeDocumentsParallelly
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim documentsToMerge As Stream() = GetDocumentsToMerge(1000)
            Dim rangeSize As Integer = 50

            While documentsToMerge.Length > rangeSize
                Dim partitionCount As Integer = Math.Ceiling(documentsToMerge.Length / rangeSize)

                Dim result = New Stream(partitionCount - 1) {}
                Dim p = Partitioner.Create(0, documentsToMerge.Length, rangeSize)
                Parallel.ForEach(p,
                    Sub(range)
                        Dim startIndex As Integer = range.Item1
                        Dim count As Integer = range.Item2 - range.Item1
                        result(startIndex / rangeSize) = MergeToStream(documentsToMerge, startIndex, count)
                    End Sub)
                documentsToMerge = result
            End While

            Dim pathToFile As String = "MergeDocumentsParallelly.pdf"
            Using final As PdfDocument = GetMergedDocument(documentsToMerge, 0, documentsToMerge.Length)
                final.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function MergeToStream(streams As Stream(), startIndex As Integer, count As Integer) As Stream
            Using pdf As PdfDocument = GetMergedDocument(streams, startIndex, count)
                Dim result = New MemoryStream()
                Dim options = New PdfSaveOptions With {
                    .UseObjectStreams = False ' speed up writing of intermediate documents
                }
                pdf.Save(result, options)
                Return result
            End Using
        End Function

        Private Shared Function GetMergedDocument(streams As Stream(), startIndex As Integer, count As Integer) As PdfDocument
            Dim pdf = New PdfDocument()
            Try
                For i As Integer = 0 To count - 1
                    Dim s = streams(startIndex + i)
                    pdf.Append(s)
                    s.Dispose()
                Next

                pdf.RemovePage(0)
                pdf.ReplaceDuplicateObjects()
                Return pdf
            Catch
                pdf.Dispose()
                Throw
            End Try
        End Function

        Private Shared Function GetDocumentsToMerge(count As Integer) As Stream()
            Dim streams = New Stream(count - 1) {}

            For i As Integer = 0 To streams.Length - 1
                streams(i) = File.OpenRead("..\Sample Data\jfif3.pdf")
            Next

            Return streams
        End Function
    End Class
End Namespace