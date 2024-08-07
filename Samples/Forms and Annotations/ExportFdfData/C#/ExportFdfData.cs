using System;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExportFdfData
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string fdfFile = "ExportFdfData.fdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                foreach (PdfControl control in pdf.GetControls())
                {
                    switch (control.Name)
                    {
                        case "login":
                            ((PdfTextBox)control).Text = "Some Name";
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
                    }
                }

                pdf.ExportFdf(fdfFile);
            }

            Console.WriteLine("FDF file is exported to " + Path.Combine(Directory.GetCurrentDirectory(), fdfFile));
        }
    }
}
