Public Class Credentials
    Public ReadOnly Name As String
    Public ReadOnly Keystore As String
    Public ReadOnly Password As String

    Public Sub New(name As String, keystore As String, password As String)
        Me.Name = name
        Me.Keystore = keystore
        Me.Password = password
    End Sub
End Class
