Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class HelloWorldSample
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            HelloWorld7.CreatePdf()

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo("hello.pdf") With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
