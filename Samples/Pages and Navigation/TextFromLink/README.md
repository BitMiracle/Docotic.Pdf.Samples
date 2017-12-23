# Extract text from link target
This sample shows how to retrieve a link information and extract text starting from the link target.

Links in PDF document are represented as PdfActionArea objects. PdfActionArea object can be used to get a page which hosts action area and area bounds. Also PdfActionArea contains the Action property. For links this property contains an instance of PdfGoToAction object. PdfGoToAction object contains a page associated with the link. 

In order to extract text from link's target page we should get the top offset on target page and then retrieve all text from target page with coordinates below the top offset.

Please note that the trial version of Docotic.Pdf loads only odd pages on document opening, so you may find that some expected links and pages are missing. Licensed version doesn't have this limitation and loads all pages and links.