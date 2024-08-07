Imports System.IO
Imports BitMiracle.Docotic

Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.Logging
Imports NLog.Extensions.Logging

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class LoggingWithNLog
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            ' In order to receive log messages from Docotic.Pdf into a NLog logger,
            ' you would need to configure NLog. It is a common practice to put NLog
            ' configuration into app.config file.
            ' Take a look into the app.config file, it contains more comments.

            ' After NLog is configured, you need to create a corresponding logger factory and
            ' pass it to LogManager class from the Docotic.Logging add-on.
            Using factory As New NLogLoggerFactory
                LogManager.UseLoggerFactory(factory)

                ' The following code should produce log messages in console and in
                ' log-file.txt file next to application's exe file.
                Using pdf As New PdfDocument("..\Sample Data\Attachments.pdf")
                    Using ms As New MemoryStream()
                        pdf.Pages(0).Save(ms, PdfDrawOptions.Create())
                    End Using
                End Using
            End Using
        End Sub
    End Class
End Namespace