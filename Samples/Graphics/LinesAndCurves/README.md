# Lines and Curves
This sample shows how to draw straight lines and cubic Bezier curves.

All drawing in a PDF document is done using methods and properties of PdfCanvas class. You can access it using Canvas property of PdfPage, PdfWatermark or PdfPattern classes.

There is a plenty of methods that draw lines, curves and shapes. All such methods have names that start with "Draw" (e.g. DrawLineTo method). All kinds of lines are drawn using current pen (PdfCanvas.Pen property).
