using System.IO;
using BitMiracle.Docotic.Pdf.Logging;
using Microsoft.Extensions.Logging;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class LoggingWithLog4Net
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            // In order to receive log messages from Docotic.Pdf into a log4net logger,
            // you would need to configure log4net. This code sample uses the
            // Microsoft.Extensions.Logging.Log4Net.AspNetCore package, which configures log4net
            // using the provided config. Look into the app.config file, it contains more comments.
            var options = new Log4NetProviderOptions { UseWebOrAppConfig = true };
            using var factory = new LoggerFactory();
            factory.AddLog4Net(options);
            LogManager.UseLoggerFactory(factory);

            // The following code should produce log messages in console and in
            // log-file.txt file next to application's exe file.
            using var pdf = new PdfDocument(@"..\Sample Data\Attachments.pdf");
            using var ms = new MemoryStream();
            pdf.Pages[0].Save(ms, PdfDrawOptions.Create());
        }
    }
}