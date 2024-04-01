using System;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExportFdfData
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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
