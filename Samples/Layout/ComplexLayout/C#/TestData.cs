using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class TestData
    {
        public static DateTime CreatedOn = new(2023, 2, 18, 14, 49, 56, DateTimeKind.Utc);
        public static string CompanyName = "Bit Miracle";
        public static string Phone = "(000) 111-1111";
        public static string Email = "support@bitmiracle.com";

        public static PatientInfo Patient = new("Bruce Wayne", new(1915, 4, 17), "M");
        public static ClinicInfo Clinic = new("Arkham Asylum", "Gotham City");
        public static AnalysisInfo Analysis = new(Guid.NewGuid().ToString(), DateTime.UtcNow, "Lorem ipsum");

        public static ResultItem[] Results = new ResultItem[]
        {
            new("Red Blood Cells", "10e12/L", (4.2, 5.4), 3),
            new("Hemoglobin", "g/L", (120, 160), 100.7),
            new("Hematocrit", "F of T", (0.370, 0.470), 0.36),
            new("MCV", "fL", (78, 100), 90),
            new("MCH", "pg", (27, 34), 27),
            new("RDW-CV", "%", (11.5, 14), 15.3),
            new("Lymphocytes/Neutrophils", null, (0.25, 0.99), 0.15),
            new("Lymphocytes %", "F of T", (0.20, 0.75), 0.1),
        };
    }

    record PatientInfo(string Name, DateTime BirthDate, string Gender);

    record ClinicInfo(string Name, string Address);

    record AnalysisInfo(string Id, DateTime Date, string Comment);

    record ResultItem(
        string Analyte,
        string? Units,
        (double Min, double Max)? Reference,
        double? Result)
    {
        public ReferenceMatchResult? FitsReferenceInterval()
        {
            if (Reference == null || Result == null)
                return null;

            var (min, max) = Reference.Value;
            if (Result < min)
                return ReferenceMatchResult.Less;

            if (Result > max)
                return ReferenceMatchResult.Greater;

            return ReferenceMatchResult.Fit;
        }
    }

    enum ReferenceMatchResult
    {
        Less,
        Fit,
        Greater,
    }
}
