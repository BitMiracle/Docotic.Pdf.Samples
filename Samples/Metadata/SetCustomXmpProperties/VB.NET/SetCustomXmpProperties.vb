Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SetCustomXmpProperties
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument()
                Dim custom As XmpSchema = pdf.Metadata.Custom

                ' add simple string property
                custom.Properties.Add("stringProperty", "string value")

                ' add simple array property
                Dim array As New XmpArray(XmpArrayType.Ordered)
                For i As Integer = 0 To 4
                    array.Values.Add(New XmpString(i.ToString()))
                Next
                custom.Properties.Add("arrayProperty", array)

                ' array array with language alternatives
                Dim languageArray As New XmpArray(XmpArrayType.Alternative)
                languageArray.Values.Add(New XmpLanguageAlternative("x-default", "I'm Feeling Lucky"))
                languageArray.Values.Add(New XmpLanguageAlternative("fr-fr", "J'ai de la chance"))
                custom.Properties.Add("arrayWithAlternatives", languageArray)

                ' add string properties with qualifiers
                Dim author1 As New XmpString("First Author")
                author1.Qualifiers.Add("role", "main author")
                custom.Properties.Add("anotherStringProperty", author1)

                Dim author2 As New XmpString("Second Author")
                author2.Qualifiers.Add("role", "co-author")
                custom.Properties.Add("yetAnotherStringProperty", author2)

                ' add array with qualified string values
                Dim authors As New XmpArray(XmpArrayType.Unordered)
                authors.Values.Add(author1)
                authors.Values.Add(author2)
                custom.Properties.Add("arrayWithQualifiedStrings", authors)

                ' add structure
                Dim struct As New XmpStructure("struct", "http://example.com/structure/")
                struct.Properties.Add("string", "string in a structure")
                struct.Properties.Add("array", authors)
                struct.Properties.Add("arrayWithAlternatives", languageArray)
                custom.Properties.Add("structureProperty", struct)

                Dim pathToFile As String = "SetCustomXmpProperties.pdf"
                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace
