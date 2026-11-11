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
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using var pdf = new PdfDocument(@"..\Sample Data\Link.pdf");
            LinkInfo? linkInfo = GetLinks(pdf).FirstOrDefault();
            if (linkInfo == null)
            {
                Console.WriteLine("The document contains no links!");
                return;
            }

            var linkDescription = new StringBuilder();
            linkDescription.AppendLine("Link is at page index: " + linkInfo.OwnerPageIndex);
            linkDescription.AppendLine("Link index in the page's widgets collection: " + linkInfo.OwnerPageWidgetIndex);
            linkDescription.AppendLine("Link bounds: " + linkInfo.Bounds);
            linkDescription.AppendLine("Target page index: " + FindPageIndex(pdf, linkInfo.TargetPage));
            linkDescription.AppendLine("Note: All page indexes are zero-based.");

            linkDescription.AppendLine();
            linkDescription.AppendLine("Text at the link destination:");
            linkDescription.AppendLine(GetTextFromLink(linkInfo.TargetPage, linkInfo.TopOffset));

            Console.WriteLine(linkDescription.ToString());
        }

        private static IEnumerable<LinkInfo> GetLinks(PdfDocument pdf)
        {
            for (int i = 0; i < pdf.Pages.Count; ++i)
            {
                PdfPage page = pdf.Pages[i];
                int widgetIndex = 0;
                foreach (var widget in page.Widgets)
                {
                    if (widget is PdfActionArea actionArea)
                    {
                        if (actionArea.Action is PdfGoToAction linkAction)
                        {
                            PdfDocumentView? view = linkAction.GetView().View;
                            if (view is not null)
                            {
                                // Skip links that point to a nonexistent page.
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

            // Allow some extra tolerance in the vertical position of the text start.
            const float eps = 5.0f;
            var textFromTargetPage = targetPage.Canvas.GetTextData();
            foreach (PdfTextData textData in textFromTargetPage)
            {
                if (textData.Position.Y < targetPage.Height - topOffset - eps)
                    continue;

                result.Append(textData.GetText() + " ");
            }

            return result.ToString();
        }

        private static int FindPageIndex(PdfDocument pdf, PdfPage page)
        {
            var pageDictionary = page?.Dictionary;
            if (pageDictionary is null)
                return -1;

            var pages = pdf.Pages;
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].Dictionary == pageDictionary)
                    return i;
            }

            return -1;
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
