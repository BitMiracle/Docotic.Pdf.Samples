using System;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextFromLink
    {
        private class LinkInfo
        {
            public readonly PdfGoToAction Action;
            public readonly int Index = -1;
            public readonly PdfRectangle Bounds;
            public readonly int OwnerPageNumber = -1;
            public readonly int TargetPageNumber = -1;

            public LinkInfo(PdfDocument pdf, PdfActionArea actionArea, int index)
            {
                if (pdf == null)
                    throw new ArgumentNullException("document");

                if (actionArea == null)
                    throw new ArgumentNullException("actionArea");

                Action = actionArea.Action as PdfGoToAction;
                if (Action == null)
                    throw new ArgumentException("Action area doesn't contain link", "actionArea");

                Index = index;
                Bounds = actionArea.BoundingBox;
                OwnerPageNumber = pdf.IndexOf(actionArea.Owner);
                TargetPageNumber = pdf.IndexOf(Action.View.Page);
            }
        }

        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\Link.pdf"))
            {
                LinkInfo linkInfo = getFirstLink(pdf);
                if (linkInfo == null)
                {
                    Console.WriteLine("Document doesn't contain links!");
                    return;
                }

                StringBuilder linkDescription = new StringBuilder();
                linkDescription.AppendLine("Link's index in document widgets collection: " + linkInfo.Index);
                linkDescription.AppendLine("Number of page with link: " + linkInfo.OwnerPageNumber);
                linkDescription.AppendLine("Link bounds: " + linkInfo.Bounds.ToString());
                linkDescription.AppendLine("Link points to page # " + linkInfo.TargetPageNumber);
                linkDescription.AppendLine("NOTE: All page numbers are zero-based. If you use trial version of Docotic.Pdf then even pages of original document are not loaded.");

                linkDescription.AppendLine();
                linkDescription.AppendLine("Text from link:");
                linkDescription.AppendLine(getTextFromLink(linkInfo.Action));

                Console.WriteLine(linkDescription.ToString());
                Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            }
        }

        private static LinkInfo getFirstLink(PdfDocument pdf)
        {
            int i = 0;
            foreach (PdfWidget widget in pdf.GetWidgets())
            {
                PdfActionArea actionArea = widget as PdfActionArea;
                if (actionArea != null)
                {
                    PdfGoToAction linkAction = actionArea.Action as PdfGoToAction;
                    if (linkAction != null)
                    {
                        // lets ignore links which point to an absent page
                        if (linkAction.View.Page != null)
                            return new LinkInfo(pdf, actionArea, i);
                    }
                }

                i++;
            }

            return null;
        }

        private static string getTextFromLink(PdfGoToAction linkAction)
        {
            PdfPage targetPage = linkAction.View.Page;
            if (targetPage == null)
                return String.Empty;

            StringBuilder result = new StringBuilder();

            const float eps = 5.0f; // small reserve for text start vertical position
            PdfCollection<PdfTextData> textFromTargetPage = targetPage.Canvas.GetTextData();
            foreach (PdfTextData textData in textFromTargetPage)
            {
                if (textData.Position.Y < targetPage.Height - linkAction.View.Top - eps)
                    continue;

                result.Append(textData.GetText() + " ");
            }

            return result.ToString();
        }
    }
}
