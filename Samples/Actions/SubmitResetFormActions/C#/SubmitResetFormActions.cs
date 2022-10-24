using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SubmitResetFormActions
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "SubmitResetFormActions.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                PdfTextBox textBox = page.AddTextBox("sample_textbox", 10, 50, 100, 15);
                textBox.Text = "Hello";

                PdfCheckBox checkBox = page.AddCheckBox("sample_checkbox", 10, 70, 100, 15, "Check me");
                checkBox.Checked = true;

                PdfRadioButton radioButton = page.AddRadioButton("radio", 10, 90, 100, 15, "Select me!");
                radioButton.ExportValue = "radio1";
                radioButton.Checked = true;

                PdfRadioButton radioButton2 = page.AddRadioButton("radio", 10, 110, 100, 15, "No, select me!");
                radioButton2.ExportValue = "radio2";
                radioButton2.Checked = false;

                PdfButton submitButton = page.AddButton(10, 130, 45, 20);
                submitButton.Text = "Submit";
                PdfSubmitFormAction submitAction = pdf.CreateSubmitFormAction(new Uri("http://bitmiracle.com/login.php"));
                submitAction.SubmitFormat = PdfSubmitFormat.Html;
                submitAction.SubmitMethod = PdfSubmitMethod.Get;
                submitAction.AddControl(textBox);
                submitAction.AddControl(checkBox);
                submitAction.AddControl(radioButton);
                submitAction.AddControl(radioButton2);
                submitButton.OnMouseDown = submitAction;

                PdfButton resetButton = page.AddButton(65, 130, 45, 20);
                resetButton.Text = "Reset";
                PdfResetFormAction resetAction = pdf.CreateResetFormAction();
                resetAction.AddControl(textBox);
                resetAction.AddControl(checkBox);
                resetAction.AddControl(radioButton);
                resetAction.AddControl(radioButton2);
                resetButton.OnMouseDown = resetAction;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
