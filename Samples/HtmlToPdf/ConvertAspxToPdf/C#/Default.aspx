<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ConvertAspxToPdf.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Select Customer</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Select Customer</h2>

        <asp:DropDownList ID="ddlCustomers" runat="server" />

        <h3>Select Purchases</h3>
        <asp:CheckBoxList ID="chkPurchases" runat="server" />

        <br /><br />
        <asp:Button ID="btnGenerateInvoice" runat="server" Text="Generate PDF Invoice" OnClick="BtnGenerateInvoice_Click" />
        <br /><br />
        <asp:Button ID="btnPreviewInvoice" runat="server" Text="Peview Invoice" OnClick="BtnPreviewInvoice_Click" />
    </form>
</body>
</html>
