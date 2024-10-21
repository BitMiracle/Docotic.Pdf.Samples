Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SetCustomXmpProperties
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "SetCustomXmpProperties.pdf"

            Using pdf As New PdfDocument()
                Dim custom As XmpSchema = pdf.Metadata.Custom

                ' add a simple string property
                custom.Properties.Add("stringProperty", "string value")

                ' add a simple array property
                Dim array As New XmpArray(XmpArrayType.Ordered)
                For i As Integer = 0 To 4
                    array.Values.Add(New XmpString(i.ToString()))
                Next
                custom.Properties.Add("arrayProperty", array)

                ' add an array with language alternatives
                Dim languageArray As New XmpArray(XmpArrayType.Alternative)
                languageArray.Values.Add(New XmpLanguageAlternative("x-default", "I'm Feeling Lucky"))
                languageArray.Values.Add(New XmpLanguageAlternative("fr-fr", "J'ai de la chance"))
                custom.Properties.Add("arrayWithAlternatives", languageArray)

                ' add some string properties with qualifiers
                Dim author1 As New XmpString("First Author")
                author1.Qualifiers.Add("role", "main author")
                custom.Properties.Add("anotherStringProperty", author1)

                Dim author2 As New XmpString("Second Author")
                author2.Qualifiers.Add("role", "co-author")
                custom.Properties.Add("yetAnotherStringProperty", author2)

                ' add an array with the qualified string values
                Dim authors As New XmpArray(XmpArrayType.Unordered)
                authors.Values.Add(author1)
                authors.Values.Add(author2)
                custom.Properties.Add("arrayWithQualifiedStrings", authors)

                ' add a structure
                Dim struct As New XmpStructure("struct", "http://example.com/structure/")
                struct.Properties.Add("string", "string in a structure")
                struct.Properties.Add("array", authors)
                struct.Properties.Add("arrayWithAlternatives", languageArray)
                custom.Properties.Add("structureProperty", struct)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
