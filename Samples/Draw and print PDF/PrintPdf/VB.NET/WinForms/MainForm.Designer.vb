Imports System

Namespace BitMiracle.Docotic.Pdf.Samples
    Partial Public Class MainForm
        Private components As ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.printButton = New System.Windows.Forms.Button()
            Me.previewButton = New System.Windows.Forms.Button()
            Me.printSize = New System.Windows.Forms.ComboBox()
            Me.SuspendLayout()
            '
            'printButton
            '
            Me.printButton.Location = New System.Drawing.Point(16, 15)
            Me.printButton.Margin = New System.Windows.Forms.Padding(4)
            Me.printButton.Name = "printButton"
            Me.printButton.Size = New System.Drawing.Size(132, 43)
            Me.printButton.TabIndex = 0
            Me.printButton.Text = "Print"
            Me.printButton.UseVisualStyleBackColor = True
            '
            'previewButton
            '
            Me.previewButton.Location = New System.Drawing.Point(181, 15)
            Me.previewButton.Margin = New System.Windows.Forms.Padding(4)
            Me.previewButton.Name = "previewButton"
            Me.previewButton.Size = New System.Drawing.Size(132, 43)
            Me.previewButton.TabIndex = 1
            Me.previewButton.Text = "Preview"
            Me.previewButton.UseVisualStyleBackColor = True
            '
            'printSize
            '
            Me.printSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.printSize.FormattingEnabled = True
            Me.printSize.Items.AddRange(New Object() {"Fit page", "Actual size"})
            Me.printSize.Location = New System.Drawing.Point(16, 77)
            Me.printSize.Name = "printSize"
            Me.printSize.Size = New System.Drawing.Size(297, 24)
            Me.printSize.TabIndex = 2
            Me.printSize.SelectedIndex = 0
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 497)
            Me.Controls.Add(Me.printSize)
            Me.Controls.Add(Me.previewButton)
            Me.Controls.Add(Me.printButton)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.Name = "MainForm"
            Me.Text = "Print PDF"
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents printButton As System.Windows.Forms.Button
        Friend WithEvents previewButton As System.Windows.Forms.Button
        Friend WithEvents printSize As System.Windows.Forms.ComboBox
    End Class
End Namespace
