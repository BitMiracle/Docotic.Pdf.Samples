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
                Console.WriteLine("Document doesn't contain links!");
                return;
            }

            var linkDescription = new StringBuilder();
            linkDescription.AppendLine("Number of page with the link: " + linkInfo.OwnerPageIndex);
            linkDescription.AppendLine("Link's index in the page widgets collection: " + linkInfo.OwnerPageWidgetIndex);
            linkDescription.AppendLine("Link bounds: " + linkInfo.Bounds.ToString());
            linkDescription.AppendLine("Link points to page # " + pdf.IndexOf(linkInfo.TargetPage));
            linkDescription.AppendLine("NOTE: All page numbers are zero-based.");

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
                                // let's ignore links which point to an absent page
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

            // allow some extra space for the vertical position of a text start
            const float eps = 5.0f;
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
