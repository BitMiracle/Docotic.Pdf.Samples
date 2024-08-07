using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class HideAction
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "HideAction.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfButton hideeButton = page.AddButton(45, 85, 70, 25);
                hideeButton.Text = "Button";

                PdfTextBox hideeTextBox = page.AddTextBox(45, 120, 70, 25);
                hideeTextBox.Text = "TextBox";

                PdfHideAction hideAction = pdf.CreateHideAction(true);
                hideAction.AddControl(hideeButton);
                hideAction.AddControl(hideeTextBox);

                PdfHideAction showAction = pdf.CreateHideAction(false);
                showAction.AddControl(hideeButton);
                showAction.AddControl(hideeTextBox);

                PdfButton showButton = page.AddButton("showBtn", 10, 50, 70, 25);
                showButton.Text = "Show controls";
                showButton.OnMouseUp = showAction;

                PdfButton hideButton = page.AddButton("hideBtn", 90, 50, 70, 25);
                hideButton.Text = "Hide controls";
                hideButton.OnMouseUp = hideAction;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
