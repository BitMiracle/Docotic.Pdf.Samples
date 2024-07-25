#if RELEASE

using System.Diagnostics;
using BitMiracle.Docotic.Pdf.Samples.Tests.Helpers;
using NUnit.Framework;

namespace BitMiracle.Docotic.Pdf.Samples.Tests
{
    [TestFixture]
    public class NativeAotTests
    {
        [TestCaseSource(typeof(SavingTests), nameof(SavingTests.OpenSaveTestCase))]
        public void OpenAndSave(string fileName, string password = "")
        {
            string outputName = $"OpenSave_{fileName}";
            RunAotCompatibility("opensave", outputName, fileName, password);

            SavingTests.CompareDocuments(outputName);
        }

        [TestCaseSource(typeof(TextExtractionTests), nameof(TextExtractionTests.ExtractTextTestCase))]
        public void ExtractPlainText(string fileName)
        {
            GetText($"{nameof(ExtractPlainText)}", fileName, false);
        }

        [TestCaseSource(typeof(TextExtractionTests), nameof(TextExtractionTests.ExtractTextTestCase))]
        public void ExtractFormattedText(string fileName)
        {
            GetText($"{nameof(ExtractFormattedText)}", fileName, true);
        }

        private static void GetText(string outputPrefix, string fileName, bool formatted)
        {
            string outputName = $"{outputPrefix}_{fileName}.txt";
            string withFormatting = formatted ? "1" : "0";
            RunAotCompatibility("gettext", outputName, fileName, withFormatting);

            TextExtractionTests.CompareText(outputName);
        }

        private static void RunAotCompatibility(string command, string outputFileName, string inputFileName, params string[] args)
        {
            const string Quote = "\"";
            var arguments = new string[args.Length + 3];
            arguments[0] = command;
            arguments[1] = Quote + outputFileName.ToOutput() + Quote;
            arguments[2] = Quote + inputFileName.ToInput() + Quote;
            for (int i = 0; i < args.Length; ++i)
                arguments[i + 3] = Quote + args[i] + Quote;

            string folder = "AotCompatibility\\bin\\Release\\net8.0\\win-x64\\publish\\win-x64";

            //// Uncomment to test regular, non-published version.
            //// You may also need to remove <RuntimeIdentifier>win-x64</RuntimeIdentifier>
            //// from AotCompatibility.csproj. Or append \win-x64 to the path below.
            ////folder = "AotCompatibility\\bin\\Release\\net8.0";

            using var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(TestPaths.PathToSolution, folder, "AotCompatibility.exe"),
                    Arguments = string.Join(" ", arguments),
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            bool processWasExited = process.WaitForExit(30000);
            if (!processWasExited)
                process.Kill();

            if (output != string.Empty)
                Console.WriteLine(output);

            if (error != string.Empty)
                Console.Error.WriteLine(error);

            Assert.True(processWasExited);
            Assert.AreEqual(0, process.ExitCode);
        }
    }
}

#endif