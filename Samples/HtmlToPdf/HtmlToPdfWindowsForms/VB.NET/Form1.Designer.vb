<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.labelUrl = New System.Windows.Forms.Label()
        Me.textBoxUrl = New System.Windows.Forms.TextBox()
        Me.buttonConvert = New System.Windows.Forms.Button()
        Me.progressBarConverting = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'labelUrl
        '
        Me.labelUrl.AutoSize = True
        Me.labelUrl.Location = New System.Drawing.Point(13, 13)
        Me.labelUrl.Name = "labelUrl"
        Me.labelUrl.Size = New System.Drawing.Size(176, 32)
        Me.labelUrl.TabIndex = 0
        Me.labelUrl.Text = "URL to convert:"
        '
        'textBoxUrl
        '
        Me.textBoxUrl.Location = New System.Drawing.Point(13, 49)
        Me.textBoxUrl.Name = "textBoxUrl"
        Me.textBoxUrl.Size = New System.Drawing.Size(556, 39)
        Me.textBoxUrl.TabIndex = 1
        '
        'buttonConvert
        '
        Me.buttonConvert.Location = New System.Drawing.Point(13, 128)
        Me.buttonConvert.Name = "buttonConvert"
        Me.buttonConvert.Size = New System.Drawing.Size(200, 46)
        Me.buttonConvert.TabIndex = 2
        Me.buttonConvert.Text = "Convert"
        Me.buttonConvert.UseVisualStyleBackColor = True
        '
        'progressBarConverting
        '
        Me.progressBarConverting.Location = New System.Drawing.Point(240, 128)
        Me.progressBarConverting.MarqueeAnimationSpeed = 50
        Me.progressBarConverting.Name = "progressBarConverting"
        Me.progressBarConverting.Size = New System.Drawing.Size(329, 46)
        Me.progressBarConverting.Step = 30
        Me.progressBarConverting.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.progressBarConverting.TabIndex = 3
        '
        'Form1
        '
        Me.AcceptButton = Me.buttonConvert
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 32.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 212)
        Me.Controls.Add(Me.progressBarConverting)
        Me.Controls.Add(Me.buttonConvert)
        Me.Controls.Add(Me.textBoxUrl)
        Me.Controls.Add(Me.labelUrl)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents labelUrl As Label
    Friend WithEvents textBoxUrl As TextBox
    Friend WithEvents buttonConvert As Button
    Friend WithEvents progressBarConverting As ProgressBar
End Class
