Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class NumericTextField
        Public Shared Sub Main()
            Dim validationJavascript As New StringBuilder()
            validationJavascript.AppendLine("function validateNumeric(event) {")
            validationJavascript.AppendLine("    var validCharacters = ""0123456789"";")
            validationJavascript.AppendLine("    for (var i = 0; i < event.change.length; i++) {")
            validationJavascript.AppendLine("        if (validCharacters.indexOf(event.change.charAt(i)) == -1) {")
            validationJavascript.AppendLine("            app.beep(0);")
            validationJavascript.AppendLine("            event.rc = false;")
            validationJavascript.AppendLine("            break;")
            validationJavascript.AppendLine("        }")
            validationJavascript.AppendLine("    }")
            validationJavascript.AppendLine("}")

            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "NumericTextField.pdf"

            Using pdf As New PdfDocument()

                pdf.SharedScripts.Add(pdf.CreateJavaScriptAction(validationJavascript.ToString()))

                Dim textBox As PdfTextBox = pdf.Pages(0).AddTextBox(20, 50, 100, 20)
                textBox.OnKeyPress = pdf.CreateJavaScriptAction("validateNumeric(event);")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
