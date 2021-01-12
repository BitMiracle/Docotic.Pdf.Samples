# Check if PDF document is password protected
This sample shows how to check if an existing PDF document is encrypted.

To get encryption information about an existing document please use the PdfDocument.GetEncryptionInfo method. The method returns null (Nothing in VB.NET) for not encrypted documents. For documents encrypted with a password, the method returns an instance of PdfStandardEncryptionInfo class. In case the document is encrypted using a certificate, the GetEncryptionInfo method returns an instance of PdfPublicKeyEncryptionInfo class. 
 
The PdfDocument.GetEncryptionInfo method is static, so you can perform a check before opening a PDF document. This is useful if you don't know beforehand if a password or a certificate is required to open a PDF document.