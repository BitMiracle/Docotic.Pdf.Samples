using BitMiracle.Docotic.Pdf.Samples.Tests.Helpers;

namespace AotCompatibility
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DocoticLicense.Apply();

            try
            {
                if (args.Length == 0)
                    throw new ArgumentException("Missing arguments. " + getSupportedOptionsInfo());

                string commandType = args[0];
                switch (commandType)
                {
                    case Commands.OpenSave:
                        openSave(args);
                        break;

                    case Commands.GetText:
                        getText(args);
                        break;

                    default:
                        throw new ArgumentException($"Invalid command type {commandType}. {getSupportedOptionsInfo()}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        static void openSave(string[] args)
        {
            if (args.Length < 4)
                throw new ArgumentException($"Wrong syntax. {getSupportedOptionsInfo()}");

            string outputPath = args[1];
            string inputPath = args[2];
            string password = args[3];
            PdfSaver.Save(inputPath, password, outputPath);
        }

        static void getText(string[] args)
        {
            if (args.Length < 4)
                throw new ArgumentException($"Wrong syntax. {getSupportedOptionsInfo()}");

            string outputPath = args[1];
            string inputPath = args[2];
            bool withFormatting = args[3] == "1";
            PdfTextExtractor.Extract(inputPath, withFormatting, outputPath);
        }

        static string getSupportedOptionsInfo()
        {
            string[] messageLines =
            {
                "Supported options:",
                $"{Commands.OpenSave} {{OutputPath}} {{InputPath}} {{InputPassword}}",
                $"{Commands.GetText} {{OutputPath}} {{InputPath}} {{WithFormatting}}, where WithFormatting should be 0 or 1",
            };
            return string.Join(Environment.NewLine, messageLines);
        }

        static class Commands
        {
            public const string GetText = "gettext";
            public const string OpenSave = "opensave";
        }
    }
}
