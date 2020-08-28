# Extract text from PDF on AWS Lambda
This sample shows how to extract text from a PDF document with non-embedded fonts in serverless environments.

By default, [Docotic.Pdf](https://bitmiracle.com/pdf-library/) uses GDI+ and System.Drawing.Font class to load system fonts. However, you cannot install GDI+ packages (libc6-dev and libgdiplus) on AWS Lambda. It means that text might be extracted incorrectly, if your document uses non-embedded fonts and you use the library on AWS Lambda.

To help library extract text properly, you need to use custom loader for non-embedded fonts. This sample shows how to use built-in DirectoryFontLoader class to load non-embedded fonts on AWS Lambda.

## Some steps to follow from Visual Studio:

Download and install AWS Toolkit from here: [https://aws.amazon.com/visualstudio/](https://aws.amazon.com/visualstudio/)

To deploy your function to AWS Lambda, right click the project in Solution Explorer and select *Publish to AWS Lambda*.

To view your deployed function, open its Function View window by double-clicking the function name shown beneath the AWS Lambda node in the AWS Explorer tree.

To perform testing against your deployed function, use the Test Invoke tab in the opened Function View window.

To configure event sources for your deployed function, for example to have your function invoked when an object is created in an Amazon S3 bucket, use the Event Sources tab in the opened Function View window.

To update the runtime configuration of your deployed function, use the Configuration tab in the opened Function View window.

To view execution logs of invocations of your function, use the Logs tab in the opened Function View window.

## Some steps to follow to get started from the command line:

Once you have edited your template and code, you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Deploy function to AWS Lambda
```
    cd "ExtractTextOnAwsLambda"
    dotnet lambda deploy-function
```

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/blog/extract-text-from-pdf-in-net) article
* [Find and highlight text in a PDF document](/Samples/Text/FindAndHighlightText) sample