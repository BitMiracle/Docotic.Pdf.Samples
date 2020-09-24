Imports System
Imports System.Windows.Forms

Namespace BitMiracle.Docotic.Samples.PrintPdf
    Friend Module Program
        <STAThread>
        Public Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New MainForm())
        End Sub
    End Module
End Namespace
