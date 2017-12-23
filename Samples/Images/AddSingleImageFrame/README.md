# Add single image frame to PDF document
This sample shows how to add single frame (page) from a multipage image to PDF document.

The PdfDocument.OpenImage method accepts path to an image file or image bytes and returns collection of image frames (pages). Each collection item is an instance of PdfImageFrame class. Any image frame can be added to a PDF document using AddImage method. Image frames can be added to a PDF document in any order. There is no need to add all loaded frames to a PDF document.