using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class GetAllAttachments
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (PdfDocument pdf = new PdfDocument(Path.Combine(location, "Attachments.pdf")))
            {
                StringBuilder attachmentNames = new StringBuilder();

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
                        attachmentNames.AppendLine(fileAnnot.File.Specification);
                    }
                }

                Console.WriteLine("Attachments List:");
                Console.WriteLine(attachmentNames.ToString());
            }
        }
    }
}
