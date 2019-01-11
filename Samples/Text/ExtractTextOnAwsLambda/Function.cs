using Amazon.Lambda.Core;
using BitMiracle.Docotic.Pdf;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ExtractTextOnAwsLambda
{
    public class Function
    {
        public string FunctionHandler(ILambdaContext context)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            PdfConfigurationOptions config = PdfConfigurationOptions.Create();
            config.FontLoader = new DirectoryFontLoader(new[] { "/usr/share/fonts" }, true);

            using (var pdf = new PdfDocument("Attachments.pdf", config))
            {
                return pdf.GetTextWithFormatting();
            }
        }
    }
}
