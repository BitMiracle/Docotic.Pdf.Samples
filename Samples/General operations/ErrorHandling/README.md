# Error handling
This sample shows how to handle PDF-specific exceptions thrown by Docotic.Pdf library.

In addition to standard .NET exceptions (such as ArgumentNullException or ArgumentOutOfRangeException) Docotic.Pdf library can throw exceptions of PdfException type. These exceptions are specific to PDF format. So, they are thrown when you try to do something that is not allowed or invalid from PDF format point of view. This sample shows one of such situations: a try to draw some text with characters which are not supported by the current font.
