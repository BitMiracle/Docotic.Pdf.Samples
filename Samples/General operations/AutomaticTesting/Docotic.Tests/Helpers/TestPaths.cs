namespace BitMiracle.Docotic.Pdf.Samples.Tests.Helpers
{
    static class TestPaths
    {
        public static readonly string PathToSolution = @"..\..\..\..\";

        public static string ToInput(this string fileName)
            => Path.Combine(PathToSolution, @"..\..\Sample Data", fileName);

        public static string ToOutput(this string fileName)
            => Path.Combine(PathToSolution, @"..\..\Output", fileName);

        public static string ToExpected(this string fileName)
            => Path.Combine(PathToSolution, "ExpectedOutput", fileName);
    }
}
