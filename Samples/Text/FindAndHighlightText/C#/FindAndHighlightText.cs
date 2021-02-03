using System;
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
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "FindAndHighlightText.pdf";

            using (var pdf = new PdfDocument(@"Sample Data\jfif3.pdf"))
            {
                const string TextToFind = "JPEG File Interchange Format";
                const StringComparison Comparison = StringComparison.InvariantCultureIgnoreCase;
                var highlightColor = new PdfRgbColor(255, 255, 0);
                
                highlightPhrases(pdf, TextToFind, Comparison, highlightColor);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }

        private static void highlightPhrases(PdfDocument pdf, string textToFind, StringComparison comparison,
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
                foreach (PdfRectangle[] phraseBounds in findPhrases(page, wordsToFind, comparison))
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

        private static IEnumerable<PdfRectangle[]> findPhrases(PdfPage page, string[] wordsToFind,
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

                        PdfRectangle intersection = getIntersectionBounds(w, index, word.Length);
                        yield return new[] { intersection };
                    }
                }

                yield break;
            }

            // We use the following algorithm:
            // 1. Group words by transformation matrix. We do that to properly detect neighbours for rotated text.
            // 2. For each group:
            //   a. Sort words taking into account the group's transformation matrix.
            //   b. Search phrase in the collection of sorted words.
            Dictionary<PdfMatrix, List<PdfTextData>> wordGroups = groupWordsByTransformations(page);
            foreach (var kvp in wordGroups)
            {
                PdfMatrix transformation = kvp.Key;
                List<PdfTextData> words = kvp.Value;

                sortWords(words, transformation);

                int i = 0;
                var foundPhrase = new PdfRectangle[wordsToFind.Length];
                foreach (PdfTextData w in words)
                {
                    string pdfText = w.GetText();
                    if (matchWord(pdfText, wordsToFind, i, comparison))
                    {
                        int wordLength = wordsToFind[i].Length;
                        int startIndex = (i == 0) ? (pdfText.Length - wordLength) : 0;
                        foundPhrase[i] = getIntersectionBounds(w, startIndex, wordLength);
                        ++i;
                        if (i < wordsToFind.Length)
                            continue;

                        yield return foundPhrase;
                    }

                    i = 0;
                }
            }
        }

        private static Dictionary<PdfMatrix, List<PdfTextData>> groupWordsByTransformations(PdfPage page)
        {
            var result = new Dictionary<PdfMatrix, List<PdfTextData>>();
            foreach (PdfTextData word in page.GetWords())
            {
                PdfMatrix matrix = normalizeScaleFactors(word.TransformationMatrix);
                matrix.OffsetX = 0;
                matrix.OffsetY = 0;

                if (result.TryGetValue(matrix, out List<PdfTextData> matrixChunks))
                    matrixChunks.Add(word);
                else
                    result[matrix] = new List<PdfTextData> { word };
            }

            return result;
        }

        private static void sortWords(List<PdfTextData> words, PdfMatrix transformation)
        {
            // For some transformations we should invert X coordinates during sorting.
            PdfPoint xAxis = transformVector(transformation, 1, 0);
            PdfPoint yAxis = transformVector(transformation, 0, 1);

            int xDirection = 1;

            // Happens when space is rotated on 90 or 270 degrees
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
                double yDiff = measureVerticalDistance(x.Position, y.Position, transformation);
                if (Math.Abs(yDiff) > 0.0001)
                    return yDiff.CompareTo(0);

                double xDiff = measureHorizontalDistance(x.Position, y.Position, transformation);
                return (xDiff * xDirection).CompareTo(0);
            });
        }

        private static PdfMatrix normalizeScaleFactors(PdfMatrix m)
        {
            double scale = getScaleFactor(m.M11, m.M12, m.M21, m.M22);

            // Round to 1 fractional digit to avoid separation of similar matrices
            // like { 1, 0, 0, 1.12, 0, 0 } and { 1, 0, 0, 1.14, 0, 0 }
            m.M11 = Math.Round(m.M11 / scale, 1);
            m.M12 = Math.Round(m.M12 / scale, 1);
            m.M21 = Math.Round(m.M21 / scale, 1);
            m.M22 = Math.Round(m.M22 / scale, 1);

            return m;
        }

        public static double getScaleFactor(params double[] values)
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

        private static double measureHorizontalDistance(PdfPoint first, PdfPoint second, PdfMatrix m)
        {
            return m.M11 * (first.X - second.X) + m.M21 * (first.Y - second.Y);
        }

        private static double measureVerticalDistance(PdfPoint first, PdfPoint second, PdfMatrix m)
        {
            return m.M12 * (first.X - second.X) + m.M22 * (first.Y - second.Y);
        }

        private static PdfPoint transformVector(PdfMatrix m, double x, double y)
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

        private static PdfRectangle getIntersectionBounds(PdfTextData data, int startIndex, int length)
        {
            string text = data.GetText();
            if (startIndex == 0 && text.Length == length)
                return data.Bounds;

            IEnumerable<PdfRectangle> charBounds = data.GetCharacters()
                .Skip(startIndex)
                .Take(length)
                .Select(c => c.Bounds);

            PdfRectangle union = charBounds.First();
            foreach (var b in charBounds.Skip(1))
                union = PdfRectangle.Union(b, union);

            return union;
        }

        private static bool matchWord(string text, string[] wordsToFind, int wordIndex, StringComparison comparison)
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
