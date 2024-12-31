Imports System.IO
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class SynchronizeData
        Public Shared Sub Run()
            Dim pathToFile = "SynchronizeData.pdf"

            Using pdf = New PdfDocument("..\Sample Data\Names.pdf")
                Dim synchronizeFields = File.ReadAllText("../Sample Data/SynchronizeFields.js")
                pdf.SharedScripts.Add(pdf.CreateJavaScriptAction(synchronizeFields))

                Dim control = pdf.GetControl("name0")
                control.OnLostFocus = pdf.CreateJavaScriptAction("synchronizeFields(""name0"", ""name1"");")

                control = pdf.GetControl("name1")
                control.OnLostFocus = pdf.CreateJavaScriptAction("synchronizeFields(""name1"", ""name0"");")

                pdf.Save(pathToFile)
            End Using

            Process.Start(New ProcessStartInfo(pathToFile) With {
                .UseShellExecute = True
            })
        End Sub
    End Class
End Namespace