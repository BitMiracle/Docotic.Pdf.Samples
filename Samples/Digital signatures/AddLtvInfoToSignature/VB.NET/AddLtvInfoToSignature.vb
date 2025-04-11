Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddLtvInfoToSignature
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputFileName = "AddLtvInfoToSignature.pdf"
            Using pdf As New PdfDocument("..\Sample Data\signed-cades.pdf")
                pdf.AddLtvInfo()

                Dim timestampOptions = New PdfSignatureTimestampOptions With {
                    .AuthorityUrl = New Uri("http://timestamp.digicert.com")
                }
                Dim incrementalOptions = New PdfSaveOptions() With {
                    .WriteIncrementally = True
                }
                pdf.TimestampAndSave(timestampOptions, outputFileName, incrementalOptions)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace