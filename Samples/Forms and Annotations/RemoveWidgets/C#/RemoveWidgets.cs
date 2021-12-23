using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemoveWidgets
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                pdf.Pages[0].Widgets.RemoveAt(1);
                pdf.Save("RemoveWidgetFromPage.pdf");

                foreach (PdfWidget widget in pdf.GetWidgets())
                    pdf.RemoveWidget(widget);

                pdf.Save("RemoveWidgets.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
