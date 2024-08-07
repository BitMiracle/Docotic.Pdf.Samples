namespace BitMiracle.Docotic.Pdf.Samples.Tests.Helpers
{
    static class DocoticLicense
    {
        public static void Apply()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");
        }
    }
}
