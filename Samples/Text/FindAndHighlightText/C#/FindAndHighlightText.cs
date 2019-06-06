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

            int i = 0;
            var foundPhrase = new PdfRectangle[wordsToFind.Length];
            foreach (PdfTextData data in page.GetWords())
            {
                if (matchWord(data.Text, wordsToFind, i, comparison))
                {
                    foundPhrase[i] = getIntersectionBounds(data, wordsToFind, i);
                    ++i;
                    if (i < wordsToFind.Length)
                        continue;

                    yield return foundPhrase;
                }

                i = 0;
            }
        }

        private static PdfRectangle getIntersectionBounds(PdfTextData data, string[] wordsToFind, int i)
        {
            string text = data.Text;
            int wordLength = wordsToFind[i].Length;
            if (text.Length == wordLength)
                return data.Bounds;

            int startIndex = (i == 0) ? (text.Length - wordLength) : 0;
            IEnumerable<PdfRectangle> charBounds = data.GetCharacters()
                .Skip(startIndex)
                .Take(wordLength)
                .Select(c => c.Bounds);

            PdfRectangle union = charBounds.First();
            foreach (var b in charBounds.Skip(1))
                union = PdfRectangle.Union(b, union);

            return union;
        }

        private static bool matchWord(string text, string[] wordsToFind, int wordIndex, StringComparison comparison)
        {
            string word = wordsToFind[wordIndex];
            if (wordIndex == 0)
                return text.EndsWith(word, comparison);

            if (wordIndex == wordsToFind.Length - 1)
                return text.StartsWith(word, comparison);

            return text.Equals(word, comparison);
        }
    }
}
