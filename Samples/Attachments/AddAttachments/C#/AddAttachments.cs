using System;
using System.IO;
using System.Reflection;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddAttachments
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToFile = "AddAttachments.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // add shared attachment to the document
                PdfFileSpecification ammerland = pdf.CreateFileAttachment(Path.Combine(location, "ammerland.jpg"));
                pdf.SharedAttachments.Add(ammerland);

                // add file attachment annotation to the first page
                PdfRectangle bounds = new PdfRectangle(20, 70, 100, 100);
                PdfFileSpecification jpeg = pdf.CreateFileAttachment(Path.Combine(location, "jpeg.pdf"));
                pdf.Pages[0].AddFileAnnotation(bounds, jpeg);
                
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
