# GoTo Action
This sample shows how to use Go-To actions.

A Go-To (or jump) action can be created using PdfDocument.CreateGoToPageAction() method. CreateGoToPageAction method returns an instance of PdfGoToAction class.

Using methods and properties of PdfGoToAction class you can specify a target page to be shown as the result of the action. You can also specify the upper-left corner of the target page to be positioned at the upper-left corner of the window using PdfGoToAction.Offset property.

A go-to action (and a number of other action types) can be used for widget event handlers (e.g. for PdfButton.OnMouseDown property).