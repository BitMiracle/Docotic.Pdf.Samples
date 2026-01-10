<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Invoice.aspx.vb" Inherits="ConvertAspxToPdf.Invoice" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Invoice</title>
    <link href="Styles/invoice.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-wrapper">

            <div class="invoice-header">
                <h2>Invoice</h2>
                <div class="invoice-meta">
                    <div>Date: <% = DateTime.Now.ToString("d") %></div>
                </div>
            </div>

            <div class="customer-box">
                <asp:Literal ID="litCustomer" runat="server" />
            </div>

            <asp:GridView ID="gvPurchases" runat="server" AutoGenerateColumns="false" CssClass="invoice-grid">
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Item" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>

            <div class="total-line">
                Total:
                <asp:Literal ID="litTotal" runat="server" />
            </div>

        </div>
    </form>
</body>
</html>
