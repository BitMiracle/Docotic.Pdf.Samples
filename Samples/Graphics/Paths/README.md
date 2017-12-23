# Paths
This sample shows how to construct and use graphics path.

Graphics path is a series of connected lines and curves. You can construct a graphics path using methods of PdfCanvas class with names that start with "Append" (e.g. AppendLineTo, AppendRectangle). All those methods do not place any marks on a canvas. Instead, they add corresponding shapes to the current graphics path.

When a graphics path is constructed, you can fill, stroke or reset it. You can also use constructed path as a clip region of a canvas.