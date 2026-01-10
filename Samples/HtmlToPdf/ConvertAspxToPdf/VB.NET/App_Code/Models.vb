<Serializable()>
Public Class Customer
    Public Property Id As Integer
    Public Property Name As String
End Class

<Serializable()>
Public Class Purchase
    Public Property Id As Integer
    Public Property Description As String
    Public Property Price As Decimal
End Class
