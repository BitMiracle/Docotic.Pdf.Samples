using System;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class GetAllAttachments
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using var pdf = new PdfDocument(@"..\Sample Data\Attachments.pdf");
            var attachmentNames = new StringBuilder();

            // Collect the names of files attached to the document.
            foreach (var entry in pdf.SharedAttachments)
                attachmentNames.AppendLine(entry.Value?.Specification);

            // Collect the names of files referenced by file attachment annotations.
            foreach (var page in pdf.Pages)
            {
                if (page == null)
                    continue;

                foreach (var widget in page.Widgets)
                {
                    if (widget?.Type != PdfWidgetType.FileAttachment)
                        continue;

                    PdfFileAttachmentAnnotation fileAnnot = (PdfFileAttachmentAnnotation)widget;
                    attachmentNames.AppendLine(fileAnnot.File?.Specification);
                }
            }

            Console.WriteLine("Attachments:");
            Console.WriteLine(attachmentNames.ToString());
        }
    }
}
