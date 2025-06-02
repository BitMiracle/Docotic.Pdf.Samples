using System;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class SynchronizeData
    {
        public static void Run()
        {
            string pathToFile = "SynchronizeData.pdf";

            static PdfControl GetControl(PdfDocument pdf, string name)
            {
                return pdf.GetControl(name) ?? throw new Exception($"Failed to get field {name}");
            }

            using (var pdf = new PdfDocument(@"..\Sample Data\Names.pdf"))
            {
                var synchronizeFields = File.ReadAllText("../Sample Data/SynchronizeFields.js");
                pdf.SharedScripts.Add(
                    pdf.CreateJavaScriptAction(synchronizeFields)
                );

                var control = GetControl(pdf, "name0");
                control.OnLostFocus = pdf.CreateJavaScriptAction("synchronizeFields(\"name0\", \"name1\");");

                control = GetControl(pdf, "name1");
                control.OnLostFocus = pdf.CreateJavaScriptAction("synchronizeFields(\"name1\", \"name0\");");

                pdf.Save(pathToFile);
            }

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
