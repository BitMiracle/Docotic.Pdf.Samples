using System;
using System.Collections.Generic;

namespace ConvertAspxToPdf
{
    public partial class Default : System.Web.UI.Page
    {
        private List<Customer> Customers;
        private List<Purchase> Purchases;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();

            if (!IsPostBack)
                BindControls();
        }

        private void LoadData()
        {
            Customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Alice Johnson" },
                new Customer { Id = 2, Name = "Bob Smith" },
                new Customer { Id = 3, Name = "Charlie Brown" }
            };

            Purchases = new List<Purchase>
            {
                new Purchase { Id = 1, Description = "Laptop", Price = 1200m },
                new Purchase { Id = 2, Description = "Mouse", Price = 25m },
                new Purchase { Id = 3, Description = "Keyboard", Price = 45m }
            };
        }

        private void BindControls()
        {
            ddlCustomers.DataSource = Customers;
            ddlCustomers.DataTextField = "Name";
            ddlCustomers.DataValueField = "Id";
            ddlCustomers.DataBind();

            chkPurchases.DataSource = Purchases;
            chkPurchases.DataTextField = "Description";
            chkPurchases.DataValueField = "Id";
            chkPurchases.DataBind();
        }

        protected void BtnGenerateInvoice_Click(object sender, EventArgs e)
        {
            NavigateToInvoice(true);
        }

        protected void BtnPreviewInvoice_Click(object sender, EventArgs e)
        {
            NavigateToInvoice(false);
        }

        private void NavigateToInvoice(bool pdfVersion)
        {
            int selectedCustomerId = int.Parse(ddlCustomers.SelectedValue);

            var selectedPurchases = new List<int>();
            foreach (var item in chkPurchases.Items)
            {
                var li = (System.Web.UI.WebControls.ListItem)item;
                if (li.Selected)
                    selectedPurchases.Add(int.Parse(li.Value));
            }

            // Pass data to next page
            Session["SelectedCustomerId"] = selectedCustomerId;
            Session["SelectedPurchases"] = selectedPurchases;
            Session["Customers"] = Customers;
            Session["Purchases"] = Purchases;
            Session["GeneratePdf"] = pdfVersion;

            Response.Redirect("Invoice.aspx");
        }
    }
}