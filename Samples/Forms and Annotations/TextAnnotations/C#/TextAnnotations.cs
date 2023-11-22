using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextAnnotations
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToFile = "TextAnnotations.pdf";
            
            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                const string content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
                PdfTextAnnotation textAnnotation = page.AddTextAnnotation(10, 50, "Docotic.Pdf Samples", content);
                textAnnotation.Icon = PdfTextAnnotationIcon.Comment;
                textAnnotation.Opened = true;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
