using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RichMediaAnnotations
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            const string PathToFile = "RichMediaAnnotations.pdf";
            
            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                var bounds = new PdfRectangle(10, 50, 200, 100);
                PdfFileSpecification sound = pdf.CreateFileAttachment(@"..\Sample Data\sound.mp3");
                PdfRichMediaAnnotation annot = page.AddRichMediaAnnotation(bounds, sound, PdfRichMediaContentType.Sound);
                annot.Activation.ActivationMode = PdfRichMediaActivationMode.OnPageOpen;

                pdf.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
