Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FindAndHighlightText
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "FindAndHighlightText.pdf"

            Using pdf As New PdfDocument("..\Sample Data\jfif3.pdf")
                Const TextToFind As String = "JPEG File Interchange Format"
                Const Comparison As StringComparison = StringComparison.InvariantCultureIgnoreCase
                Dim highlightColor = New PdfRgbColor(255, 255, 0)

                HighlightPhrases(pdf, TextToFind, Comparison, highlightColor)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub HighlightPhrases(
            pdf As PdfDocument,
            textToFind As String,
            comparison As StringComparison,
            highlightColor As PdfColor
            )
            If String.IsNullOrEmpty(textToFind) Then
                Throw New ArgumentNullException(NameOf(textToFind))
            End If

            Dim wordsToFind As String() = textToFind _
                .Split(" "c) _
                .Where(Function(w) Not String.IsNullOrEmpty(w)) _
                .ToArray()
            For Each page As PdfPage In pdf.Pages
                For Each phraseBounds As PdfRectangle() In FindPhrases(page, wordsToFind, comparison)
                    Dim annot As PdfHighlightAnnotation = page.AddHighlightAnnotation("", phraseBounds(0), highlightColor)
                    If phraseBounds.Length > 1 Then
                        annot.SetTextBounds(phraseBounds.[Select](Function(b) CType(b, PdfQuadrilateral)))
                    End If
                Next
            Next
        End Sub

        Private Shared Iterator Function FindPhrases(
            page As PdfPage,
            wordsToFind As String(),
            comparison As StringComparison
            ) As IEnumerable(Of PdfRectangle())

            If wordsToFind.Length = 0 Then Return

            If wordsToFind.Length = 1 Then
                Dim word As String = wordsToFind(0)

                For Each w As PdfTextData In page.GetWords()
                    Dim index As Integer = -1
                    Dim pdfText As String = w.GetText()
                    While True
                        index = pdfText.IndexOf(word, index + 1, comparison)
                        If index < 0 Then Exit While

                        Dim intersection As PdfRectangle = GetIntersectionBounds(w, pdfText, index, word.Length)
                        Yield {intersection}
                    End While
                Next

                Return
            End If

            ' We use the following algorithm:
            ' 1. Group words by transformation matrix. We do that to properly detect neighbours for rotated text.
            ' 2. For each group:
            '   a. Sort words taking into account the group's transformation matrix.
            '   b. Search phrase in the collection of sorted words.
            Dim wordGroups As Dictionary(Of PdfMatrix, List(Of PdfTextData)) = GroupWordsByTransformations(page)
            For Each kvp In wordGroups
                Dim transformation As PdfMatrix = kvp.Key
                Dim words As List(Of PdfTextData) = kvp.Value

                SortWords(words, transformation)

                Dim i As Integer = 0
                Dim foundPhrase = New PdfRectangle(wordsToFind.Length - 1) {}
                For Each w As PdfTextData In words

                    Dim pdfText As String = w.GetText()
                    If MatchWord(pdfText, wordsToFind, i, comparison) Then
                        Dim wordLength As Integer = wordsToFind(i).Length
                        Dim startIndex As Integer = If((i = 0), (pdfText.Length - wordLength), 0)
                        foundPhrase(i) = GetIntersectionBounds(w, pdfText, startIndex, wordLength)
                        i += 1
                        If i < wordsToFind.Length Then Continue For

                        Yield foundPhrase
                    End If

                    i = 0
                Next
            Next
        End Function

        Private Shared Function GroupWordsByTransformations(
            page As PdfPage
            ) As Dictionary(Of PdfMatrix, List(Of PdfTextData))

            Dim result = New Dictionary(Of PdfMatrix, List(Of PdfTextData))()

            For Each word As PdfTextData In page.GetWords()
                Dim matrix As PdfMatrix = NormalizeScaleFactors(word.TransformationMatrix)
                matrix.OffsetX = 0
                matrix.OffsetY = 0

                Dim matrixChunks As List(Of PdfTextData) = Nothing
                If result.TryGetValue(matrix, matrixChunks) Then
                    matrixChunks.Add(word)
                Else
                    result(matrix) = New List(Of PdfTextData) From {word}
                End If
            Next

            Return result
        End Function

        Private Shared Sub SortWords(
            words As List(Of PdfTextData),
            transformation As PdfMatrix
            )

            ' For some transformations we should invert X coordinates during sorting.
            Dim xAxis As PdfPoint = TransformVector(transformation, 1, 0)
            Dim yAxis As PdfPoint = TransformVector(transformation, 0, 1)
            Dim xDirection As Integer = 1

            ' Happens when space is rotated on 90 or 270 degrees
            If Math.Abs(xAxis.X) < 0.0001 Then
                ' Happens when transformed coordinates fall to 2nd Or 4th quarter.
                '          y
                '          ^
                '   2nd    |   1st
                '          |
                ' ------------------> x
                '          |
                '   3rd    |   4th
                '          |
                '
                If Math.Sign(xAxis.Y) <> Math.Sign(yAxis.X) Then xDirection = -1
            End If

            words.Sort(
                Function(x, y)
                    Dim yDiff As Double = MeasureVerticalDistance(x.Position, y.Position, transformation)
                    If Math.Abs(yDiff) > 0.0001 Then Return yDiff.CompareTo(0)

                    Dim xDiff As Double = MeasureHorizontalDistance(x.Position, y.Position, transformation)
                    Return (xDiff * xDirection).CompareTo(0)
                End Function
            )
        End Sub

        Private Shared Function NormalizeScaleFactors(m As PdfMatrix) As PdfMatrix
            Dim scale As Double = GetScaleFactor(m.M11, m.M12, m.M21, m.M22)

            ' Round to 1 fractional digit to avoid separation of similar matrices
            ' Like { 1, 0, 0, 1.12, 0, 0 } And { 1, 0, 0, 1.14, 0, 0 }
            m.M11 = Math.Round(m.M11 / scale, 1)
            m.M12 = Math.Round(m.M12 / scale, 1)
            m.M21 = Math.Round(m.M21 / scale, 1)
            m.M22 = Math.Round(m.M22 / scale, 1)
            Return m
        End Function

        Private Shared Function GetScaleFactor(ParamArray values As Double()) As Double
            Dim result As Double = Double.MaxValue

            For Each val As Double In values
                Dim abs As Double = Math.Abs(val)
                If abs > 0.001 AndAlso abs < result Then result = abs
            Next

            Return result
        End Function

        Private Shared Function MeasureHorizontalDistance(
            first As PdfPoint,
            second As PdfPoint,
            m As PdfMatrix
            ) As Double

            Return m.M11 * (first.X - second.X) + m.M21 * (first.Y - second.Y)
        End Function

        Private Shared Function MeasureVerticalDistance(
            first As PdfPoint,
            second As PdfPoint,
            m As PdfMatrix
            ) As Double

            Return m.M12 * (first.X - second.X) + m.M22 * (first.Y - second.Y)
        End Function

        Private Shared Function TransformVector(
            m As PdfMatrix,
            x As Double,
            y As Double
            ) As PdfPoint

            ' Multiply
            '           | m11     m12      0 |
            ' |x y 1| * | m21     m22      0 | = | (m11 * x + m21 * y + offsetX) (m12 * x + m22 * y + offsetY) |
            '           | offsetX offsetY  1 |
            Return New PdfPoint(m.M11 * x + m.M21 * y + m.OffsetX, m.M12 * x + m.M22 * y + m.OffsetY)
        End Function

        Private Shared Function GetIntersectionBounds(
            data As PdfTextData,
            text As String,
            startIndex As Integer,
            length As Integer
            ) As PdfRectangle

            If startIndex = 0 AndAlso text.Length = length Then
                Return data.Bounds
            End If

            Dim characters As IEnumerable(Of PdfTextData) = data.GetCharacters()
            If (Not ContainsLtrCharactersOnly(data, text)) Then
                ' Process right-to-left chunks in the reverse order.
                '
                ' Note that this approach might not work for chunks
                ' containing both left-to-right and right-to-left characters.
                characters = characters.Reverse()
            End If

            Dim charBounds As IEnumerable(Of PdfRectangle) = characters _
                .Skip(startIndex) _
                .Take(length) _
                .[Select](Function(c) c.Bounds)

            Dim union As PdfRectangle = charBounds.First()
            For Each b In charBounds.Skip(1)
                union = PdfRectangle.Union(b, union)
            Next

            Return union
        End Function

        Private Shared Function ContainsLtrCharactersOnly(data As PdfTextData, textLogical As String) As Boolean
            Dim noBidiOptions = New PdfTextConversionOptions With {.UseBidi = False}
            Dim textVisual As String = data.GetText(noBidiOptions)
            Return textLogical = textVisual
        End Function

        Private Shared Function MatchWord(
            text As String,
            wordsToFind As String(),
            wordIndex As Integer,
            comparison As StringComparison
            ) As Boolean

            Debug.Assert(wordsToFind.Length > 1)

            Dim word As String = wordsToFind(wordIndex)
            If wordIndex = 0 Then
                Return text.EndsWith(word, comparison)
            End If

            If wordIndex = wordsToFind.Length - 1 Then
                Return text.StartsWith(word, comparison)
            End If

            Return text.Equals(word, comparison)
        End Function
    End Class
End Namespace
