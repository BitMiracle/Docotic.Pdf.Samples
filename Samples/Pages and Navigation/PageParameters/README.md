# Page parameters
This sample shows how to setup a page size, orientation or rotation.

You can specify size of any page in pixels using PdfPage.Width and PdfPage.Height properties. You can also use PdfPage.Size property to specify size of a page using a value from PdfPaperSize enumeration. This enumeration defines a lot of predefined paper formats like A4, Letter or Envelope.

Please use PdfPage.Orientation property to specify orientation of a page (portrait or landscape).

You can specify a rotation for a page using PdfPage.Rotation property. Rotation can be 90, 180 or 270 degrees in clockwise direction.