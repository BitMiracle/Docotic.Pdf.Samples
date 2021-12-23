using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemovePages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage firstPage = pdf.Pages[0];
                firstPage.Canvas.DrawString(10, 50, "Main page");

                //add page to the end and then delete it
                pdf.AddPage();
                pdf.RemovePage(pdf.PageCount - 1);

                //another way to delete page
                PdfPage pageToDelete = pdf.AddPage();
                pdf.RemovePage(pageToDelete);

                //we can delete several pages at once
                pdf.InsertPage(0);
                pdf.InsertPage(1);
                pdf.RemovePages(new int[] { 0, 1 });

                pdf.Save("RemovePages.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
