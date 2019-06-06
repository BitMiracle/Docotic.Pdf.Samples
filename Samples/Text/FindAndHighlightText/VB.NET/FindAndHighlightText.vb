Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FindAndHighlightText
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "FindAndHighlightText.pdf"

            Using pdf As New PdfDocument("Sample Data\jfif3.pdf")
                Const TextToFind As String = "JPEG File Interchange Format"
                Const Comparison As StringComparison = StringComparison.InvariantCultureIgnoreCase
                Dim highlightColor = New PdfRgbColor(255, 255, 0)

                highlightPhrases(pdf, TextToFind, Comparison, highlightColor)

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub

        Private Shared Sub highlightPhrases(
            ByVal pdf As PdfDocument,
            ByVal textToFind As String,
            ByVal comparison As StringComparison,
            ByVal highlightColor As PdfColor
            )
            If String.IsNullOrEmpty(textToFind) Then
                Throw New ArgumentNullException(NameOf(textToFind))
            End If

            Dim wordsToFind As String() = textToFind _
                .Split(" "c) _
                .Where(Function(w) Not String.IsNullOrEmpty(w)) _
                .ToArray()
            For Each page As PdfPage In pdf.Pages
                For Each phraseBounds As PdfRectangle() In findPhrases(page, wordsToFind, comparison)
                    Dim annot As PdfHighlightAnnotation = page.AddHighlightAnnotation("", phraseBounds(0), highlightColor)
                    If phraseBounds.Length > 1 Then
                        annot.SetTextBounds(phraseBounds.[Select](Function(b) CType(b, PdfQuadrilateral)))
                    End If
                Next
            Next
        End Sub

        Private Shared Iterator Function findPhrases(
            ByVal page As PdfPage,
            ByVal wordsToFind As String(),
            ByVal comparison As StringComparison) As IEnumerable(Of PdfRectangle()
            )
            If wordsToFind.Length = 0 Then Return

            Dim i As Integer = 0
            Dim foundPhrase = New PdfRectangle(wordsToFind.Length - 1) {}
            For Each data As PdfTextData In page.GetWords()
                If matchWord(data.Text, wordsToFind, i, comparison) Then
                    foundPhrase(i) = getIntersectionBounds(data, wordsToFind, i)
                    i += 1
                    If i < wordsToFind.Length Then
                        Continue For
                    End If

                    Yield foundPhrase
                End If

                i = 0
            Next
        End Function

        Private Shared Function getIntersectionBounds(ByVal data As PdfTextData, ByVal wordsToFind As String(), ByVal i As Integer) As PdfRectangle
            Dim text As String = data.Text
            Dim wordLength As Integer = wordsToFind(i).Length
            If text.Length = wordLength Then
                Return data.Bounds
            End If

            Dim startIndex As Integer = If((i = 0), (text.Length - wordLength), 0)
            Dim charBounds As IEnumerable(Of PdfRectangle) = data _
                .GetCharacters() _
                .Skip(startIndex) _
                .Take(wordLength) _
                .[Select](Function(c) c.Bounds)

            Dim union As PdfRectangle = charBounds.First()
            For Each b In charBounds.Skip(1)
                union = PdfRectangle.Union(b, union)
            Next

            Return union
        End Function

        Private Shared Function matchWord(
            ByVal text As String,
            ByVal wordsToFind As String(),
            ByVal wordIndex As Integer,
            ByVal comparison As StringComparison
            ) As Boolean
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
