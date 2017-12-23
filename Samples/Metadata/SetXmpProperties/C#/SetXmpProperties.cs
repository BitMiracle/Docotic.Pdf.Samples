using System.Diagnostics;
using System.Drawing;
using System.IO;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SetXmpProperties
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.Metadata.DublinCore.Creators = new XmpArray(XmpArrayType.Ordered);
                pdf.Metadata.DublinCore.Creators.Values.Add(new XmpString("me"));
                pdf.Metadata.DublinCore.Creators.Values.Add(new XmpString("Docotic.Pdf"));

                pdf.Metadata.DublinCore.Format = new XmpString("application/pdf");

                pdf.Metadata.Pdf.Producer = new XmpString("me too!");

                string pathToFile = "SetXmpProperties.pdf";
                pdf.Save(pathToFile);
                Process.Start(pathToFile);
            }
        }
    }
}
