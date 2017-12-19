using System.Diagnostics;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Listboxes
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToFile = "Listboxes.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfListBox listBox = page.AddListBox(10, 50, 120, 80);
                listBox.AddItem("First item");
                listBox.AddItem("Second item");
                listBox.AddItem("Third item");
                listBox.MultiSelection = true;

                PdfListItem[] items =
                {
                    new PdfListItem("One", "1"),
                    new PdfListItem("Two", "2"),
                    new PdfListItem("Three", "3"),
                };
                PdfListBox listBox2 = page.AddListBox(10, 140, 120, 80);
                listBox2.AddItems(items);
                listBox2.SelectItems(new PdfListItem[] { items[0], items[2] });

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
