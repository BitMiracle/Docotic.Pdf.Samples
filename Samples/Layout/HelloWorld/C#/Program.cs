using System.Diagnostics;
using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class HelloWorldSample
    {
        static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            HelloWorld7.CreatePdf();

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo("hello.pdf") { UseShellExecute = true });
        }
    }
}