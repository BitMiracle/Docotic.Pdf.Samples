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
                // When generating the PDF version of the invoice, the code calls the default
                // implementation of the Render method, providing its own instance of the
                // HtmlTextWriter class.

                // The Render method produces the HTML for the invoice. When it finishes, the
                // CreatePdfResponse method converts that HTML into a PDF and updates the response
                // so it delivers the PDF instead of the HTML.

                var sw = new StringWriter();
                var htmlWriter = new HtmlTextWriter(sw);

                base.Render(htmlWriter);

                string html = sw.ToString();
                CreatePdfResponse(html, "invoice.pdf");
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

                // To force the browser to start downloading the PDF, change `inline` to
                // `attachment`
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
                // The application usually doesn't have write permissions for the current working
                // folder. As a result, the converter won't be able to download Google Chrome
                // there. Let's use the temp folder as the location for the browser instead.
                var engineOptions = new HtmlEngineOptions() { Path = Path.GetTempPath() };

                using (var converter = await HtmlConverter.CreateAsync(engineOptions))
                {
                    var conversionOptions = new HtmlConversionOptions();

                    // Without the base URL, all relative paths in the HTML will break. As a
                    // result, images, CSS styles, and other resources won't appear in the PDF.
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