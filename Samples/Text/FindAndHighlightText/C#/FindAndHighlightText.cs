﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FindAndHighlightText
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "FindAndHighlightText.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                const string TextToFind = "JPEG File Interchange Format";
                const StringComparison Comparison = StringComparison.InvariantCultureIgnoreCase;
                var highlightColor = new PdfRgbColor(255, 255, 0);

                HighlightPhrases(pdf, TextToFind, Comparison, highlightColor);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void HighlightPhrases(PdfDocument pdf, string textToFind, StringComparison comparison,
            PdfColor highlightColor)
        {
            if (string.IsNullOrEmpty(textToFind))
                throw new ArgumentNullException(nameof(textToFind));

            string[] wordsToFind = textToFind
                    .Split(' ')
                    .Where(w => !string.IsNullOrEmpty(w))
                    .ToArray();
            foreach (PdfPage page in pdf.Pages)
            {
                foreach (PdfRectangle[] phraseBounds in FindPhrases(page, wordsToFind, comparison))
                {
                    PdfHighlightAnnotation annot = page.AddHighlightAnnotation("", phraseBounds[0], highlightColor);
                    if (phraseBounds.Length > 1)
                        annot.SetTextBounds(phraseBounds.Select(b => (PdfQuadrilateral)b));

                    // Or you can highlight the text using vector graphics:
                    //page.Canvas.Brush.Color = highlightColor;
                    //page.Canvas.Brush.Opacity = 50;
                    //foreach (var bounds in phraseBounds)
                    //    page.Canvas.DrawRectangle(bounds, PdfDrawMode.Fill);
                }
            }
        }

        private static IEnumerable<PdfRectangle[]> FindPhrases(PdfPage page, string[] wordsToFind,
            StringComparison comparison)
        {
            if (wordsToFind.Length == 0)
                yield break;

            if (wordsToFind.Length == 1)
            {
                string word = wordsToFind[0];
                foreach (PdfTextData w in page.GetWords())
                {
                    string pdfText = w.GetText();
                    int index = -1;
                    for (;;)
                    {
                        index = pdfText.IndexOf(word, index + 1, comparison);
                        if (index < 0)
                            break;

                        PdfRectangle intersection = GetIntersectionBounds(w, pdfText, index, word.Length);
                        yield return new[] { intersection };
                    }
                }

                yield break;
            }

            // We use the following algorithm:
            // 1. Group words by transformation matrix. We do that to detect
            // neighbours for rotated text properly.
            // 2. For each group:
            //   a. Sort words taking the group's transformation matrix into account.
            //   b. Search phrase in the collection of sorted words.
            Dictionary<PdfMatrix, List<PdfTextData>> wordGroups = GroupWordsByTransformations(page);
            foreach (var kvp in wordGroups)
            {
                PdfMatrix transformation = kvp.Key;
                List<PdfTextData> words = kvp.Value;

                SortWords(words, transformation);

                int i = 0;
                var foundPhrase = new PdfRectangle[wordsToFind.Length];
                foreach (PdfTextData w in words)
                {
                    string pdfText = w.GetText();
                    if (MatchWord(pdfText, wordsToFind, i, comparison))
                    {
                        int wordLength = wordsToFind[i].Length;
                        int startIndex = (i == 0) ? (pdfText.Length - wordLength) : 0;
                        foundPhrase[i] = GetIntersectionBounds(w, pdfText, startIndex, wordLength);
                        ++i;
                        if (i < wordsToFind.Length)
                            continue;

                        yield return foundPhrase;
                    }

                    i = 0;
                }
            }
        }

        private static Dictionary<PdfMatrix, List<PdfTextData>> GroupWordsByTransformations(PdfPage page)
        {
            var result = new Dictionary<PdfMatrix, List<PdfTextData>>();
            foreach (PdfTextData word in page.GetWords())
            {
                PdfMatrix matrix = NormalizeScaleFactors(word.TransformationMatrix);
                matrix.OffsetX = 0;
                matrix.OffsetY = 0;

                if (result.TryGetValue(matrix, out List<PdfTextData>? matrixChunks))
                    matrixChunks.Add(word);
                else
                    result[matrix] = new List<PdfTextData> { word };
            }

            return result;
        }

        private static void SortWords(List<PdfTextData> words, PdfMatrix transformation)
        {
            // For some transformations, we should invert X coordinates during sorting.
            PdfPoint xAxis = TransformVector(transformation, 1, 0);
            PdfPoint yAxis = TransformVector(transformation, 0, 1);

            int xDirection = 1;

            // Happens when space is rotated by 90 or 270 degrees
            if (Math.Abs(xAxis.X) < 0.0001)
            {
                // Happens when transformed coordinates fall to 2nd or 4th quarter.
                //          y
                //          ^
                //   2nd    |   1st
                //          |
                // ------------------> x
                //          |
                //   3rd    |   4th
                //          |
                //
                if (Math.Sign(xAxis.Y) != Math.Sign(yAxis.X))
                    xDirection = -1;
            }

            words.Sort((x, y) =>
            {
                double yDiff = MeasureVerticalDistance(x.Position, y.Position, transformation);
                if (Math.Abs(yDiff) > 0.0001)
                    return yDiff.CompareTo(0);

                double xDiff = MeasureHorizontalDistance(x.Position, y.Position, transformation);
                return (xDiff * xDirection).CompareTo(0);
            });
        }

        private static PdfMatrix NormalizeScaleFactors(PdfMatrix m)
        {
            double scale = GetScaleFactor(m.M11, m.M12, m.M21, m.M22);

            // Round to 1 fractional digit to avoid separation of similar matrices
            // like { 1, 0, 0, 1.12, 0, 0 } and { 1, 0, 0, 1.14, 0, 0 }
            m.M11 = Math.Round(m.M11 / scale, 1);
            m.M12 = Math.Round(m.M12 / scale, 1);
            m.M21 = Math.Round(m.M21 / scale, 1);
            m.M22 = Math.Round(m.M22 / scale, 1);

            return m;
        }

        public static double GetScaleFactor(params double[] values)
        {
            double result = double.MaxValue;
            foreach (double val in values)
            {
                double abs = Math.Abs(val);
                if (abs > 0.001 && abs < result)
                    result = abs;
            }

            return result;
        }

        private static double MeasureHorizontalDistance(PdfPoint first, PdfPoint second, PdfMatrix m)
        {
            return m.M11 * (first.X - second.X) + m.M21 * (first.Y - second.Y);
        }

        private static double MeasureVerticalDistance(PdfPoint first, PdfPoint second, PdfMatrix m)
        {
            return m.M12 * (first.X - second.X) + m.M22 * (first.Y - second.Y);
        }

        private static PdfPoint TransformVector(PdfMatrix m, double x, double y)
        {
            // Multiply:
            //           | m11     m12      0 |
            // |x y 1| * | m21     m22      0 | = | (m11 * x + m21 * y + offsetX) (m12 * x + m22 * y + offsetY) |
            //           | offsetX offsetY  1 |

            return new PdfPoint(
                m.M11 * x + m.M21 * y + m.OffsetX,
                m.M12 * x + m.M22 * y + m.OffsetY
            );
        }

        private static PdfRectangle GetIntersectionBounds(PdfTextData data, string text, int startIndex, int length)
        {
            if (startIndex == 0 && text.Length == length)
                return data.Bounds;

            IEnumerable<PdfTextData> characters = data.GetCharacters();
            if (!ContainsLtrCharactersOnly(data, text))
            {
                // Process right-to-left chunks in the reverse order.
                //
                // Note that this approach might not work for chunks
                // containing both left-to-right and right-to-left characters.
                characters = characters.Reverse();
            }

            IEnumerable<PdfRectangle> charBounds = characters
                .Skip(startIndex)
                .Take(length)
                .Select(c => c.Bounds);

            PdfRectangle union = charBounds.First();
            foreach (var b in charBounds.Skip(1))
                union = PdfRectangle.Union(b, union);

            return union;
        }

        private static bool ContainsLtrCharactersOnly(PdfTextData data, string textLogical)
        {
            // A simple trick to detect right-to-left / bidirectional text.
            // Compare logical and visual representations of text to detect directionality.
            var noBidiOptions = new PdfTextConversionOptions { UseBidi = false };
            string textVisual = data.GetText(noBidiOptions);
            return textLogical == textVisual;
        }

        private static bool MatchWord(string text, string[] wordsToFind, int wordIndex, StringComparison comparison)
        {
            Debug.Assert(wordsToFind.Length > 1);

            string word = wordsToFind[wordIndex];
            if (wordIndex == 0)
                return text.EndsWith(word, comparison);

            if (wordIndex == wordsToFind.Length - 1)
                return text.StartsWith(word, comparison);

            return text.Equals(word, comparison);
        }
    }
}
