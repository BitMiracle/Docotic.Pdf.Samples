using System;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class GetAllAttachments
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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
