# Header and Footer
This sample shows how to add common header and footer to PDF document.

You can add text, images, vector graphics to the header and footer of a PDF page. It's important to properly calculate position and orientation of the content because PDF page might be rotated.

Use PdfPage.Rotation property to transform the page's coordinate space before drawing the content. This sample shows how to add centered text to the header and right-aligned page numbers to the footer. The same technique might be used for any type of content and positioning.
