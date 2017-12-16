# Save (extract) attachment
This sample shows how to save attachment bytes (extract attached file).

Each attachment is represented by a PdfFileSpecification object. Use PdfFileSpecification.Contents property to retrieve reference to a PdfEmbeddedFile object. 

If retrieved reference is null then the file specification refers to an external file (i.e. only reference to a file is attached). 

If retrieved reference is non-null then the file is actually embedded in the document. Use can use PdfEmbeddedFile.Save method to extract contents of the attachment to a stream or a file.