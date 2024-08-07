using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Listboxes
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Listboxes.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfListBox listBox = page.AddListBox(10, 50, 120, 80);
                listBox.AddItem("First item");
                listBox.AddItem("Second item");
                listBox.AddItem("Third item");
                listBox.MultiSelection = true;

                PdfListItem[] items =
                {
                    new("One", "1"),
                    new("Two", "2"),
                    new("Three", "3"),
                };
                PdfListBox listBox2 = page.AddListBox(10, 140, 120, 80);
                listBox2.AddItems(items);
                listBox2.SelectItems(new PdfListItem[] { items[0], items[2] });

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
