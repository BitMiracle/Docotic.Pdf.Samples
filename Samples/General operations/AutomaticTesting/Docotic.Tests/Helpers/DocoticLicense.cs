namespace BitMiracle.Docotic.Pdf.Samples.Tests.Helpers
{
    static class DocoticLicense
    {
        public static void Apply()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.
            //
            //LicenseManager.AddLicenseData(..);

            if (!LicenseManager.HasValidLicense)
            {
                throw new InvalidOperationException("Please provide a Docotic.Pdf license key. Visit " +
                    "https://bitmiracle.com/pdf-library/trial-restrictions for more information.");
            }
        }
    }
}
