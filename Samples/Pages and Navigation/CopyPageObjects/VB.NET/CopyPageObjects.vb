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
                Using copy As PdfDocument = pdf.Pages.CopyRange(0, 1)
                    Dim sourcePage As PdfPage = copy.Pages(0)
                    Dim copyPage As PdfPage = copy.Pages.Add()

                    Dim options = New PdfObjectExtractionOptions

                    ' Set to true to convert searchable text into vector paths.
                    options.ExtractTextAsPath = False

                    ' Set to true to ignore marked content, such as layers and tagged PDF data.
                    '
                    ' This sample does not preserve all structural information because page
                    ' objects are copied to a different page. See the EditPageContent sample for
                    ' an example of copying page objects within the same page while preserving all
                    ' structural information.
                    options.FlattenMarkedContent = False

                    ' Usually, leave this option set to false to ensure proper handling of
                    ' documents with transparency groups. Set to true for backward compatibility
                    ' Or when transparency handling Is Not a concern.
                    options.FlattenXObjects = False

                    Dim copier = New PageObjectCopier(copy, options)
                    copier.Copy(sourcePage, copyPage)

                    copy.Pages.RemoveAt(0)

                    copy.Save(PathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace