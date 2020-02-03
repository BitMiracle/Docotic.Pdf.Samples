Imports System

Namespace BitMiracle.Docotic.Samples.PrintPdf
    Partial Public Class MainForm
        Private components As ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            printButton = New System.Windows.Forms.Button()
            previewButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            printButton.Location = New System.Drawing.Point(12, 12)
            printButton.Name = "printButton"
            printButton.Size = New System.Drawing.Size(99, 35)
            printButton.TabIndex = 0
            printButton.Text = "Print"
            printButton.UseVisualStyleBackColor = True
            AddHandler printButton.Click, New System.EventHandler(AddressOf Me.printButton_Click)
            previewButton.Location = New System.Drawing.Point(136, 12)
            previewButton.Name = "previewButton"
            previewButton.Size = New System.Drawing.Size(99, 35)
            previewButton.TabIndex = 1
            previewButton.Text = "Preview"
            previewButton.UseVisualStyleBackColor = True
            AddHandler previewButton.Click, New System.EventHandler(AddressOf Me.previewButton_Click)
            AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            AutoScaleMode = Windows.Forms.AutoScaleMode.Font
            ClientSize = New System.Drawing.Size(594, 404)
            Me.Controls.Add(Me.previewButton)
            Me.Controls.Add(Me.printButton)
            Name = "MainForm"
            Text = "Print PDF"
            Me.ResumeLayout(False)
        End Sub

        Private printButton As Windows.Forms.Button
        Private previewButton As Windows.Forms.Button
    End Class
End Namespace
