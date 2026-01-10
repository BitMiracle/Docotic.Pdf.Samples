Imports System.IO
Imports System.Threading.Tasks
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Public Class Invoice
    Inherits System.Web.UI.Page

    Private GeneratePdf As Boolean

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadInvoice()
        End If
    End Sub

    Protected Overrides Sub Render(writer As HtmlTextWriter)
        If GeneratePdf Then
            Dim sw As New StringWriter()
            Dim htmlWriter As New HtmlTextWriter(sw)

            MyBase.Render(htmlWriter)

            Dim html As String = sw.ToString()
            CreatePdfResponse(html, "invoice.pdf")
            writer.Write(html)

        Else
            MyBase.Render(writer)
        End If

    End Sub

    Private Sub LoadInvoice()
        GeneratePdf = CBool(Session("GeneratePdf"))

        Dim customers As List(Of Customer) = CType(Session("Customers"), List(Of Customer))
        Dim purchases As List(Of Purchase) = CType(Session("Purchases"), List(Of Purchase))

        Dim customerId As Integer = CInt(Session("SelectedCustomerId"))
        Dim selectedPurchaseIds As List(Of Integer) = CType(Session("SelectedPurchases"), List(Of Integer))

        Dim customer = customers.First(Function(c) c.Id = customerId)
        Dim selectedPurchases = purchases.
            Where(Function(p) selectedPurchaseIds.Contains(p.Id)).
            ToList()

        litCustomer.Text = $"Customer: <strong>{customer.Name}</strong>"

        gvPurchases.DataSource = selectedPurchases
        gvPurchases.DataBind()

        litTotal.Text = selectedPurchases.Sum(Function(p) p.Price).ToString("C")
    End Sub

    Private Sub CreatePdfResponse(html As String, outputName As String)
        Using pdfData As New MemoryStream()
            ConvertHtmlToPdfStream(html, pdfData)

            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", $"inline; filename={outputName}")

            pdfData.Position = 0
            pdfData.CopyTo(Response.OutputStream)

            Response.End()
        End Using
    End Sub

    Private Sub ConvertHtmlToPdfStream(html As String, ms As MemoryStream)
        BitMiracle.Docotic.LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

        Dim siteBase As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
        Task.Run(Async Function()
                     Dim engineOptions As New HtmlEngineOptions() With {
                        .Path = Path.GetTempPath()
                     }

                     Using converter = Await HtmlConverter.CreateAsync(engineOptions)
                         Dim conversionOptions As New HtmlConversionOptions()
                         conversionOptions.Load.BaseUri = New Uri(siteBase)

                         Using pdf = Await converter.CreatePdfFromStringAsync(html, conversionOptions)
                             pdf.Save(ms)
                         End Using

                     End Using
                 End Function
        ).GetAwaiter().GetResult()
    End Sub

End Class