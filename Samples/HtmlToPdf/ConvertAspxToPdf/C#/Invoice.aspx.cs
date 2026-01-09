using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace ConvertAspxToPdf
{
    public partial class Invoice : Page
    {
        private bool GeneratePdf;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadInvoice();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (GeneratePdf)
            {
                var sw = new StringWriter();
                var htmlWriter = new HtmlTextWriter(sw);

                base.Render(htmlWriter);

                string html = sw.ToString();
                CreatePdfResponse(html, "invoice.pdf");
                writer.Write(html);
            }
            else
            {
                base.Render(writer);
            }
        }

        private void LoadInvoice()
        {
            GeneratePdf = (bool)Session["GeneratePdf"];

            var customers = (List<Customer>)Session["Customers"];
            var purchases = (List<Purchase>)Session["Purchases"];

            int customerId = (int)Session["SelectedCustomerId"];
            var selectedPurchaseIds = (List<int>)Session["SelectedPurchases"];

            var customer = customers.First(c => c.Id == customerId);
            var selectedPurchases = purchases.Where(p => selectedPurchaseIds.Contains(p.Id)).ToList();

            litCustomer.Text = $"Customer: <strong>{customer.Name}</strong>";

            gvPurchases.DataSource = selectedPurchases;
            gvPurchases.DataBind();

            litTotal.Text = selectedPurchases.Sum(p => p.Price).ToString("C");
        }

        private void CreatePdfResponse(string html, string outputName)
        {
            using (var pdfData = new MemoryStream())
            {
                ConvertHtmlToPdfStream(html, pdfData);

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", $"inline; filename={outputName}");

                pdfData.Position = 0;
                pdfData.CopyTo(Response.OutputStream);

                Response.End();
            }
        }

        private void ConvertHtmlToPdfStream(string html, MemoryStream ms)
        {
            BitMiracle.Docotic.LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var siteBase = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            Task.Run(async () =>
            {
                var engineOptions = new HtmlEngineOptions() { Path = Path.GetTempPath() };
                using (var converter = await HtmlConverter.CreateAsync(engineOptions))
                {
                    var conversionOptions = new HtmlConversionOptions();
                    conversionOptions.Load.BaseUri = new Uri(siteBase);
                    using (var pdf = await converter.CreatePdfFromStringAsync(html, conversionOptions))
                    {
                        pdf.Save(ms);
                    }
                }
            }).GetAwaiter().GetResult();
        }
    }
}