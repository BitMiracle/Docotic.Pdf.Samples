using System.Diagnostics;
using System.Windows.Forms;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SaveAttachment
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument(@"Sample Data\Attachments.pdf"))
            {
                PdfFileSpecification spec = pdf.SharedAttachments["File Attachment testing.doc"];
                if (spec != null && spec.Contents != null)
                {
                    const string pathToFile = "attachment.doc";
                    spec.Contents.Save(pathToFile);
                    Process.Start(pathToFile);
                }
                else
                {
                    MessageBox.Show("Can't save shared attachment", "Error");
                }
            }
        }
    }
}
