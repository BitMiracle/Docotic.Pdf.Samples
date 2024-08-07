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

            // collect names of files attached to the document
            foreach (PdfFileSpecification spec in pdf.SharedAttachments)
                attachmentNames.AppendLine(spec.Specification);

            // collect names of files used in file attachment annotations
            foreach (PdfPage page in pdf.Pages)
            {
                foreach (PdfWidget widget in page.Widgets)
                {
                    if (widget.Type != PdfWidgetType.FileAttachment)
                        continue;

                    PdfFileAttachmentAnnotation fileAnnot = (PdfFileAttachmentAnnotation)widget;
                    attachmentNames.AppendLine(fileAnnot.File?.Specification);
                }
            }

            Console.WriteLine("Attachments List:");
            Console.WriteLine(attachmentNames.ToString());
        }
    }
}
