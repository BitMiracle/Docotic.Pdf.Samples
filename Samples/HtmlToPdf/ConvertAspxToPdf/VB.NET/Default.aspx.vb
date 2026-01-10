Public Class _Default
    Inherits System.Web.UI.Page

    Private Property Customers As List(Of Customer)
        Get
            Return CType(ViewState("Customers"), List(Of Customer))
        End Get
        Set(value As List(Of Customer))
            ViewState("Customers") = value
        End Set
    End Property

    Private Property Purchases As List(Of Purchase)
        Get
            Return CType(ViewState("Purchases"), List(Of Purchase))
        End Get
        Set(value As List(Of Purchase))
            ViewState("Purchases") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadData()

        If Not IsPostBack Then
            BindControls()
        End If
    End Sub

    Private Sub LoadData()
        Customers = New List(Of Customer) From {
            New Customer With {.Id = 1, .Name = "Alice Johnson"},
            New Customer With {.Id = 2, .Name = "Bob Smith"},
            New Customer With {.Id = 3, .Name = "Charlie Brown"}
        }

        Purchases = New List(Of Purchase) From {
            New Purchase With {.Id = 1, .Description = "Laptop", .Price = 1200D},
            New Purchase With {.Id = 2, .Description = "Mouse", .Price = 25D},
            New Purchase With {.Id = 3, .Description = "Keyboard", .Price = 45D}
        }
    End Sub

    Private Sub BindControls()
        ddlCustomers.DataSource = Customers
        ddlCustomers.DataTextField = "Name"
        ddlCustomers.DataValueField = "Id"
        ddlCustomers.DataBind()

        chkPurchases.DataSource = Purchases
        chkPurchases.DataTextField = "Description"
        chkPurchases.DataValueField = "Id"
        chkPurchases.DataBind()
    End Sub

    Protected Sub BtnGenerateInvoice_Click(sender As Object, e As EventArgs)
        NavigateToInvoice(True)
    End Sub

    Protected Sub BtnPreviewInvoice_Click(sender As Object, e As EventArgs)
        NavigateToInvoice(False)
    End Sub

    Private Sub NavigateToInvoice(pdfVersion As Boolean)
        Dim selectedCustomerId As Integer = Integer.Parse(ddlCustomers.SelectedValue)

        Dim selectedPurchases As New List(Of Integer)
        For Each item As ListItem In chkPurchases.Items
            If item.Selected Then
                selectedPurchases.Add(Integer.Parse(item.Value))
            End If
        Next

        ' Pass data to next page
        Session("SelectedCustomerId") = selectedCustomerId
        Session("SelectedPurchases") = selectedPurchases
        Session("Customers") = Customers
        Session("Purchases") = Purchases
        Session("GeneratePdf") = pdfVersion

        Response.Redirect("Invoice.aspx")
    End Sub

End Class