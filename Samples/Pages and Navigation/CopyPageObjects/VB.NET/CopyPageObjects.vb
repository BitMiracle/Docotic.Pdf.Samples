Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CopyPageObjects
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "CopyPageObjects.pdf"

            Using pdf = New PdfDocument("..\Sample Data\BRAILLE CODES WITH TRANSLATION.pdf")
                Using copy As PdfDocument = pdf.CopyPages(0, 1)
                    Dim sourcePage As PdfPage = copy.Pages(0)
                    Dim copyPage As PdfPage = copy.AddPage()

                    Dim options = New PdfObjectExtractionOptions

                    ' Set this option to true if you need to convert searchable text to vector paths
                    options.ExtractTextAsPath = False

                    ' Set this option to true to ignore marked content (such as layers Or tagged data)
                    '
                    ' This sample does Not preserve all structure information because page objects are
                    ' copied to another page. Look at the EditPageContent sample that shows how to copy
                    ' page objects to the same page and preserve all structure information.
                    options.FlattenMarkedContent = False

                    ' Usually, you need to set this option to false. That allows to handle
                    ' documents with transparency groups properly.
                    ' The value = true is recommended for backward compatibility or if you do not
                    ' worry about transparency.
                    options.FlattenXObjects = False

                    Dim copier = New PageObjectCopier(copy, options)
                    copier.Copy(sourcePage, copyPage)

                    copy.RemovePage(0)

                    copy.Save(PathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace