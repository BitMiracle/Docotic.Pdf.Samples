using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ReadComboBoxItems
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument(@"..\Sample data\ComboBoxes.pdf"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (PdfWidget widget in pdf.Pages[0].Widgets)
                {
                    PdfComboBox comboBox = widget as PdfComboBox;
                    if (comboBox != null)
                    {
                        sb.Append("Combobox '");
                        sb.Append(comboBox.Name);
                        sb.Append("' contains following items:\n");

                        foreach (PdfListItem item in comboBox.Items)
                        {
                            sb.Append(item);
                            sb.Append("\n");
                        }

                        sb.Append("\n");
                    }
                }

                if (sb.Length == 0)
                    Console.WriteLine("No combo boxes found on first page");
                else
                    Console.WriteLine(sb.ToString());
            }
        }
    }
}
