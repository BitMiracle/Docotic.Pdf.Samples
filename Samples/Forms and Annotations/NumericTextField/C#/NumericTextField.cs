using System;
using System.Diagnostics;
using System.Text;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class NumericTextField
    {
        public static void Main()
        {
            var validationJavascript = new StringBuilder();
            validationJavascript.AppendLine("function validateNumeric(event) {");
            validationJavascript.AppendLine("    var validCharacters = \"0123456789\";");
            validationJavascript.AppendLine("    for (var i = 0; i < event.change.length; i++) {");
            validationJavascript.AppendLine("        if (validCharacters.indexOf(event.change.charAt(i)) == -1) {");
            validationJavascript.AppendLine("            app.beep(0);");
            validationJavascript.AppendLine("            event.rc = false;");
            validationJavascript.AppendLine("            break;");
            validationJavascript.AppendLine("        }");
            validationJavascript.AppendLine("    }");
            validationJavascript.AppendLine("}");

            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "NumericTextField.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.SharedScripts.Add(
                    pdf.CreateJavaScriptAction(validationJavascript.ToString())
                );

                PdfTextBox textBox = pdf.Pages[0].AddTextBox(20, 50, 100, 20);
                textBox.OnKeyPress = pdf.CreateJavaScriptAction("validateNumeric(event);");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
