Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class LoggingWithNLog
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            ' In order to receive log messages from Docotic.Pdf into a NLog logger, 
            ' you would need to configure NLog. It is a common practice to put NLog 
            ' configuration into app.config file. 
            ' Take a look into the app.config file, it contains more comments.

            ' After NLog is configured, there is nothing else to do, the library
            ' will put its log messages into the configured loggers. 
            ' The following code should produce log messages in console and in 
            ' log-file.txt file next to application's exe file.
            Using pdf As New PdfDocument("..\Sample Data\Attachments.pdf")
                Using ms As New MemoryStream()
                    pdf.Pages(0).Save(ms, PdfDrawOptions.Create())
                End Using
            End Using
        End Sub
    End Class
End Namespace