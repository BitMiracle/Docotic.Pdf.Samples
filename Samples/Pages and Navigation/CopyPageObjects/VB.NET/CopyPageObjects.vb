Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CopyPageObjects
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "CopyPageObjects.pdf"

            Using pdf = New PdfDocument("..\Sample Data\BRAILLE CODES WITH TRANSLATION.pdf")
                Using copy As PdfDocument = pdf.CopyPages(0, 1)
                    Dim sourcePage As PdfPage = copy.Pages(0)
                    Dim copyPage As PdfPage = copy.AddPage()

                    Dim options = New PdfObjectExtractionOptions

                    ' Set this option to true if you need to convert searchable text to vector paths
                    options.ExtractTextAsPath = False

                    ' Usually, you need to set this option to false. That allows to properly handle
                    ' documents with transparency groups.
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
        End Sub
    End Class
End Namespace