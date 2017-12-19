# Numeric text fields
This sample shows how to create text fields that allow only digits to be input.

In order to create such text fields a special JavaScript action should be used (see more samples about actions in corresponding section). The action must be associated with PdfTextBox.OnKeyPress event. After that any non-digit characters input in the text field will be filtered out. Note that it will only work if Javascript is supported and turned on in your PDF Viewer.
