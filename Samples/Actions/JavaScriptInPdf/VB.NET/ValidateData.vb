Imports System.IO
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class ValidateData
        Private Const TopMargin As Double = 50   ' the height of the top margin of the document
        Private Const RightMargin As Double = 50 ' the width of the right margin of the document

        Private Const FontSize As Double = 10 ' font size in the document

        Private Shared ReadOnly Months As String() = {
            "January", "February", "March", "April", "May", "June", "July", "August",
            "September", "October", "November", "December"
        }

        Public Shared Sub Run()
            Dim pathToFile = "ValidateData.pdf"

            Using pdf = New PdfDocument()
                Dim page = pdf.Pages(0)

                SetupFont(page.Canvas, pdf.CreateFont("Arial"))

                Dim validateNumeric = File.ReadAllText("../Sample Data/ValidateNumeric.js")
                Dim action = pdf.CreateJavaScriptAction(validateNumeric)
                AddDateField(page, action)

                Dim setCurrentDate = File.ReadAllText("../Sample Data/SetCurrentDate.js")
                pdf.OnOpenDocument = pdf.CreateJavaScriptAction(setCurrentDate)

                pdf.Save(pathToFile)
            End Using

            Process.Start(New ProcessStartInfo(pathToFile) With {
                .UseShellExecute = True
            })
        End Sub

        Private Shared Sub SetupFont(canvas As PdfCanvas, font As PdfFont)
            canvas.Font = font
            canvas.FontSize = FontSize
        End Sub

        Private Shared Sub AddDateField(page As PdfPage, action As PdfJavaScriptAction)
            page.Canvas.FontSize = FontSize

            Dim left As Double = page.Width - RightMargin - 150
            Dim height = AddDayField(page, action, left, TopMargin)
            AddMonthField(page, height)
            AddYearField(page, action, height)
        End Sub

        Private Shared Function AddDayField(page As PdfPage, action As PdfJavaScriptAction, left As Double, top As Double) As Double
            Dim canvas = page.Canvas
            canvas.DrawString(left, top, "<< ")

            Dim dayTextBox As PdfTextBox = page.AddTextBox("day", canvas.TextPosition, canvas.MeasureText("00"))
            SetupTextBox(dayTextBox, canvas.Font)
            dayTextBox.MaxLength = 2
            dayTextBox.OnKeyPress = action

            canvas.TextPosition = New PdfPoint(canvas.TextPosition.X + dayTextBox.Width, canvas.TextPosition.Y)

            canvas.DrawString(" >> ")
            Return dayTextBox.Height
        End Function

        Private Shared Sub AddMonthField(page As PdfPage, height As Double)
            Dim canvas = page.Canvas
            Dim width = Double.MinValue
            For Each m In Months
                Dim monthWidth As Double = canvas.GetTextWidth("   " & m & "   ")
                If monthWidth > width Then width = monthWidth
            Next

            canvas.CurrentPosition = New PdfPoint(canvas.TextPosition.X, canvas.TextPosition.Y + height)
            canvas.DrawLineTo(canvas.TextPosition.X + width, canvas.TextPosition.Y + height)

            Dim monthTextBox As PdfTextBox = page.AddTextBox("month", canvas.TextPosition, New PdfSize(width, height))
            SetupTextBox(monthTextBox, canvas.Font)

            canvas.TextPosition = New PdfPoint(canvas.TextPosition.X + width, canvas.TextPosition.Y)
            canvas.DrawString("   ")
        End Sub

        Private Shared Sub AddYearField(page As PdfPage, action As PdfJavaScriptAction, height As Double)
            Dim canvas = page.Canvas
            Dim width As Double = canvas.GetTextWidth("0000")

            canvas.CurrentPosition = New PdfPoint(canvas.TextPosition.X, canvas.TextPosition.Y + height)
            canvas.DrawLineTo(canvas.TextPosition.X + width, canvas.TextPosition.Y + height)

            Dim yearTextBox As PdfTextBox = page.AddTextBox("year", canvas.TextPosition, New PdfSize(width, height))
            SetupTextBox(yearTextBox, canvas.Font)
            yearTextBox.MaxLength = 4
            yearTextBox.OnKeyPress = action
        End Sub

        Private Shared Sub SetupTextBox(textBox As PdfTextBox, font As PdfFont)
            textBox.Font = font
            textBox.FontSize = FontSize
            textBox.Border.Width = 0
            textBox.TextAlign = PdfTextAlign.Center
        End Sub
    End Class
End Namespace
