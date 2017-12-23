# Graphics state
This sample shows how to save and restore graphics state using PdfCanvas.SaveState and PdfCanvas.RestoreState methods.

PdfCanvas maintains a set of properties, such as pen, brush, current positions for text and graphics, font, e.t.c. This set of properties is called graphics state.

Current graphics state can be saved and previously saved state can be restored. Use PdfCanvas.SaveState() method to save current graphics state and PdfCanvas.RestoreState() method to restore last saved graphics state.
