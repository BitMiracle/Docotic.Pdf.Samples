using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FillForm
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "FillForm.pdf";

            using (var pdf = new PdfDocument(@"..\Sample data\form.pdf"))
            {
                foreach (PdfControl control in pdf.GetControls())
                {
                    switch (control.Name)
                    {
                        case "login":
                            ((PdfTextBox)control).Text = "Some Name";
                            break;

                        case "passwd":
                        case "passwd2":
                            PdfTextBox password = (PdfTextBox)control;
                            password.Text = "Password";
                            password.PasswordField = true;
                            break;

                        case "email":
                            ((PdfTextBox)control).Text = "email@gmail.com";
                            break;

                        case "user_viewemail":
                            ((PdfCheckBox)control).Checked = false;
                            break;

                        case "user_sorttopics":
                            // Check the radio button "Show new topics first"
                            PdfRadioButton sortTopics = (PdfRadioButton)control;
                            if (sortTopics.ExportValue == "user_not_sorttopics")
                                sortTopics.Checked = true;

                            break;

                        case "BtnRegister":
                            ((PdfButton)control).ReadOnly = true;
                            break;
                    }
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}