# Add one image frame to PDF document in C# and VB.NET

This sample shows how to add one page (frame) from a multi-page image to a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The [PdfDocument.OpenImage](https://api.docotic.com/pdfdocument-openimage) method can read the image data in a file, stream or bytes array and return the collection of image frames (pages). Each collection item is an instance of the [PdfImageFrame](https://api.docotic.com/pdfimageframe) class. 

Use [the AddImage method](https://api.docotic.com/pdfdocument-addimage#pdfimageframe_) to add any image frame to a PDF document. You can add image frames in any order. There is no need to add all the frames.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)