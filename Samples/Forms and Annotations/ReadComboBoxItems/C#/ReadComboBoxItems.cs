using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ReadComboBoxItems
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            using var pdf = new PdfDocument(@"..\Sample data\ComboBoxes.pdf");
            var sb = new System.Text.StringBuilder();
            foreach (PdfWidget widget in pdf.Pages[0].Widgets)
            {
                if (widget is PdfComboBox comboBox)
                {
                    sb.Append("Combobox '");
                    sb.Append(comboBox.Name);
                    sb.Append("' contains following items:\n");

                    foreach (PdfListItem item in comboBox.Items)
                    {
                        sb.Append(item);
                        sb.Append('\n');
                    }

                    sb.Append('\n');
                }
            }

            if (sb.Length == 0)
                Console.WriteLine("No combo boxes found on first page");
            else
                Console.WriteLine(sb.ToString());
        }
    }
}
