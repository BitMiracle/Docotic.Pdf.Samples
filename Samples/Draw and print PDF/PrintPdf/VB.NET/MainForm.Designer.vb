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
            printSize = New System.Windows.Forms.ComboBox()
            Me.SuspendLayout()
            '
            ' printButton
            '
            printButton.Location = New System.Drawing.Point(16, 15)
            printButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            printButton.Name = "printButton"
            printButton.Size = New System.Drawing.Size(132, 43)
            printButton.TabIndex = 0
            printButton.Text = "Print"
            printButton.UseVisualStyleBackColor = True
            AddHandler printButton.Click, New System.EventHandler(AddressOf Me.printButton_Click)
            '
            ' previewButton
            '
            previewButton.Location = New System.Drawing.Point(181, 15)
            previewButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            previewButton.Name = "previewButton"
            previewButton.Size = New System.Drawing.Size(132, 43)
            previewButton.TabIndex = 1
            previewButton.Text = "Preview"
            previewButton.UseVisualStyleBackColor = True
            AddHandler previewButton.Click, New System.EventHandler(AddressOf Me.previewButton_Click)
            '
            ' printSize
            '
            printSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            printSize.FormattingEnabled = True
            Me.printSize.Items.AddRange(New Object() {"Fit page", "Actual size"})
            printSize.Location = New System.Drawing.Point(16, 77)
            printSize.Name = "printSize"
            printSize.Size = New System.Drawing.Size(297, 24)
            printSize.TabIndex = 2
            printSize.SelectedIndex = 0
            '
            ' MainForm
            '
            AutoScaleDimensions = New System.Drawing.SizeF(8.0F, 16.0F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            ClientSize = New System.Drawing.Size(792, 497)
            Me.Controls.Add(Me.printSize)
            Me.Controls.Add(Me.previewButton)
            Me.Controls.Add(Me.printButton)
            Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Name = "MainForm"
            Text = "Print PDF"
            Me.ResumeLayout(False)
        End Sub

        Private printButton As System.Windows.Forms.Button
        Private previewButton As System.Windows.Forms.Button
        Private printSize As System.Windows.Forms.ComboBox
    End Class
End Namespace
