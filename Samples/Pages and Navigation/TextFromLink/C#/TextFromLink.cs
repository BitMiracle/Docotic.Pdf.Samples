using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextFromLink
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using var pdf = new PdfDocument(@"..\Sample Data\Link.pdf");
            LinkInfo? linkInfo = GetLinks(pdf).FirstOrDefault();
            if (linkInfo == null)
            {
                Console.WriteLine("Document doesn't contain links!");
                return;
            }

            var linkDescription = new StringBuilder();
            linkDescription.AppendLine("Number of page with the link: " + linkInfo.OwnerPageIndex);
            linkDescription.AppendLine("Link's index in the page widgets collection: " + linkInfo.OwnerPageWidgetIndex);
            linkDescription.AppendLine("Link bounds: " + linkInfo.Bounds.ToString());
            linkDescription.AppendLine("Link points to page # " + pdf.IndexOf(linkInfo.TargetPage));
            linkDescription.AppendLine("NOTE: All page numbers are zero-based. If you use trial version of Docotic.Pdf then even pages of original document are not loaded.");

            linkDescription.AppendLine();
            linkDescription.AppendLine("Text from link:");
            linkDescription.AppendLine(GetTextFromLink(linkInfo.TargetPage, linkInfo.TopOffset));

            Console.WriteLine(linkDescription.ToString());
        }

        private static IEnumerable<LinkInfo> GetLinks(PdfDocument pdf)
        {
            for (int i = 0; i < pdf.PageCount; ++i)
            {
                PdfPage page = pdf.Pages[i];
                int widgetIndex = 0;
                foreach (PdfWidget widget in page.Widgets)
                {
                    if (widget is PdfActionArea actionArea)
                    {
                        if (actionArea.Action is PdfGoToAction linkAction)
                        {
                            PdfDocumentView? view = linkAction.View;
                            if (view is not null)
                            {
                                // lets ignore links which point to an absent page
                                PdfPage? targetPage = view.Page;
                                if (targetPage is not null)
                                {
                                    yield return new LinkInfo(
                                        i, widgetIndex, actionArea.BoundingBox, targetPage, view.Top);
                                }
                            }
                        }
                    }

                    ++widgetIndex;
                }
            }
        }

        private static string GetTextFromLink(PdfPage targetPage, double? topOffset)
        {
            var result = new StringBuilder();

            const float eps = 5.0f; // small reserve for text start vertical position
            PdfCollection<PdfTextData> textFromTargetPage = targetPage.Canvas.GetTextData();
            foreach (PdfTextData textData in textFromTargetPage)
            {
                if (textData.Position.Y < targetPage.Height - topOffset - eps)
                    continue;

                result.Append(textData.GetText() + " ");
            }

            return result.ToString();
        }

        private class LinkInfo
        {
            public readonly int OwnerPageIndex;
            public readonly int OwnerPageWidgetIndex;
            public readonly PdfRectangle Bounds;
            public readonly PdfPage TargetPage;
            public readonly double? TopOffset;

            public LinkInfo(
                int ownerPageIndex, int ownerWidgetIndex, PdfRectangle linkBounds, PdfPage targetPage, double? topOffset)
            {
                OwnerPageIndex = ownerPageIndex;
                OwnerPageWidgetIndex = ownerWidgetIndex;
                Bounds = linkBounds;
                TargetPage = targetPage;
                TopOffset = topOffset;
            }
        }
    }
}
