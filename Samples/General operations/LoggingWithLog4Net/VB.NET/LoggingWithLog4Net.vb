Imports System.IO

Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.Logging
Imports Microsoft.Extensions.Logging

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class LoggingWithLog4Net
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            ' In order to receive log messages from Docotic.Pdf into a log4net logger,
            ' you would need to configure log4net. This code sample uses the
            ' Microsoft.Extensions.Logging.Log4Net.AspNetCore package, which configures log4net
            ' using the provided config. Look into the app.config file, it contains more comments.
            Dim options = New Log4NetProviderOptions With {
                .UseWebOrAppConfig = True
            }
            Using factory As New LoggerFactory()
                factory.AddLog4Net(options)
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