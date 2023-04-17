Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class TestData
        Public Shared CreatedOn = New Date(2023, 2, 18, 14, 49, 56, DateTimeKind.Utc)
        Public Shared CompanyName = "Bit Miracle"
        Public Shared Phone = "(000) 111-1111"
        Public Shared Email = "support@bitmiracle.com"

        Public Shared Patient = New PatientInfo With {.Name = "Bruce Wayne", .BirthDate = New Date(1915, 4, 17), .Gender = "M"}
        Public Shared Clinic = New ClinicInfo With {.Name = "Arkham Asylum", .Address = "Gotham City"}
        Public Shared Analysis = New AnalysisInfo With {.Id = Guid.NewGuid().ToString(), .AnalysisDate = Date.UtcNow, .Comment = "Lorem ipsum"}


        Public Shared Results = New ResultItem() {
            New ResultItem("Red Blood Cells", "10e12/L", New ReferenceInterval(4.2, 5.4), 3),
            New ResultItem("Hemoglobin", "g/L", New ReferenceInterval(120, 160), 100.7),
            New ResultItem("Hematocrit", "F of T", New ReferenceInterval(0.37, 0.47), 0.36),
            New ResultItem("MCV", "fL", New ReferenceInterval(78, 100), 90),
            New ResultItem("MCH", "pg", New ReferenceInterval(27, 34), 27),
            New ResultItem("RDW-CV", "%", New ReferenceInterval(11.5, 14), 15.3),
            New ResultItem("Lymphocytes/Neutrophils", Nothing, New ReferenceInterval(0.25, 0.99), 0.15),
            New ResultItem("Lymphocytes %", "F of T", New ReferenceInterval(0.2, 0.75), 0.1)
        }
    End Class

    NotInheritable Class PatientInfo
        Public Name As String
        Public BirthDate As Date
        Public Gender As String
    End Class

    NotInheritable Class ClinicInfo
        Public Name As String
        Public Address As String
    End Class

    NotInheritable Class AnalysisInfo
        Public Id As String
        Public AnalysisDate As Date
        Public Comment As String
    End Class

    NotInheritable Class ResultItem
        Public Sub New(analyte As String, units As String, reference As ReferenceInterval?, result As Double?)
            Me.Analyte = analyte
            Me.Units = units
            Me.Reference = reference
            Me.Result = result
        End Sub

        Public Analyte As String
        Public Units As String
        Public Reference As ReferenceInterval?
        Public Result As Double?

        Public Function FitsReferenceInterval() As ReferenceMatchResult?
            If Reference Is Nothing Or Result Is Nothing Then Return Nothing

            Dim r = Reference.Value
            If Result < r.Min Then Return ReferenceMatchResult.Less

            If Result > r.Max Then Return ReferenceMatchResult.Greater

            Return ReferenceMatchResult.Fit
        End Function
    End Class

    Structure ReferenceInterval
        Public Sub New(min As Double, max As Double)
            Me.Min = min
            Me.Max = max
        End Sub

        Public Min As Double
        Public Max As Double
    End Structure

    Enum ReferenceMatchResult
        Less
        Fit
        Greater
    End Enum
End Namespace

