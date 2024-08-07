using System.IO;
using BitMiracle.Docotic.Pdf.Logging;
using NLog.Extensions.Logging;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class LoggingWithNLog
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            // In order to receive log messages from Docotic.Pdf into a NLog logger,
            // you would need to configure NLog. It is a common practice to put NLog
            // configuration into app.config file.
            // Take a look into the app.config file, it contains more comments.

            // After NLog is configured, you need to create a corresponding logger factory and
            // pass it to LogManager class from the Docotic.Logging add-on.
            using var factory = new NLogLoggerFactory();
            LogManager.UseLoggerFactory(factory);

            // The following code should produce log messages in console and in
            // log-file.txt file next to application's exe file.
            using var pdf = new PdfDocument(@"..\Sample Data\Attachments.pdf");
            using var ms = new MemoryStream();
            pdf.Pages[0].Save(ms, PdfDrawOptions.Create());
        }
    }
}