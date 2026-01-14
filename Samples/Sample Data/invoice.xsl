<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:inv="http://example.com/invoice"
    exclude-result-prefixes="inv">

  <xsl:output method="html" indent="yes" />

  <xsl:template match="/inv:Invoice">
    <html>
      <head>
        <title>Invoice <xsl:value-of select="inv:Header/inv:InvoiceNumber"/></title>

        <style>
          body {
            font-family: Arial, sans-serif;
            margin: 40px;
            background: #f7f9fc;
            color: #333;
          }

          h1 {
            margin-bottom: 0;
            color: #2a4d8f;
          }

          h2 {
            color: #1a3a6e;
            border-bottom: 2px solid #d0d8e8;
            padding-bottom: 4px;
          }

          table {
            border-collapse: collapse;
            width: 100%;
            margin-top: 20px;
            background: #ffffff;
          }

          th {
            background: #2a4d8f;
            color: white;
            padding: 10px;
            text-align: left;
          }

          td {
            border: 1px solid #c7d1e2;
            padding: 8px;
          }

          /* Right-align numeric columns */
          td.numeric {
            text-align: right;
            font-variant-numeric: tabular-nums;
          }

          tr:nth-child(even) {
            background: #eef2f7;
          }

          .section {
            margin-top: 30px;
            background: #ffffff;
            padding: 15px;
            border-radius: 6px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.08);
          }

          .summary-row {
            display: flex;
            justify-content: space-between;
            padding: 6px 0;
            border-bottom: 1px solid #d0d8e8;
          }

          .summary-row:last-child {
            border-bottom: none;
          }

          .label {
            font-weight: bold;
            color: #1a3a6e;
          }

          .summary-value {
            font-weight: bold;
            color: #2a4d8f;
          }
        </style>
      </head>

      <body>

        <h1>Invoice</h1>

        <p><strong>Number:</strong> <xsl:value-of select="inv:Header/inv:InvoiceNumber"/></p>
        <p><strong>Date:</strong> <xsl:value-of select="inv:Header/inv:InvoiceDate"/></p>
        <p><strong>Due:</strong> <xsl:value-of select="inv:Header/inv:DueDate"/></p>

        <div class="section">
          <h2>Seller</h2>
          <p><xsl:value-of select="inv:Seller/inv:Name"/></p>
          <p>
            <xsl:value-of select="inv:Seller/inv:Address/inv:Street"/><br/>
            <xsl:value-of select="inv:Seller/inv:Address/inv:City"/>,
            <xsl:value-of select="inv:Seller/inv:Address/inv:State"/>
            <xsl:value-of select="inv:Seller/inv:Address/inv:PostalCode"/><br/>
            <xsl:value-of select="inv:Seller/inv:Address/inv:Country"/>
          </p>
        </div>

        <div class="section">
          <h2>Buyer</h2>
          <p><xsl:value-of select="inv:Buyer/inv:Name"/></p>
          <p>
            <xsl:value-of select="inv:Buyer/inv:Address/inv:Street"/><br/>
            <xsl:value-of select="inv:Buyer/inv:Address/inv:City"/>,
            <xsl:value-of select="inv:Buyer/inv:Address/inv:State"/>
            <xsl:value-of select="inv:Buyer/inv:Address/inv:PostalCode"/><br/>
            <xsl:value-of select="inv:Buyer/inv:Address/inv:Country"/>
          </p>
        </div>

        <div class="section">
          <h2>Items</h2>
          <table>
            <tr>
              <th>Description</th>
              <th>Quantity</th>
              <th>Unit Price</th>
              <th>Total</th>
            </tr>

            <xsl:for-each select="inv:Items/inv:Item">
              <tr>
                <td><xsl:value-of select="inv:Description"/></td>
                <td class="numeric"><xsl:value-of select="inv:Quantity"/></td>
                <td class="numeric"><xsl:value-of select="inv:UnitPrice"/></td>
                <td class="numeric"><xsl:value-of select="inv:Total"/></td>
              </tr>
            </xsl:for-each>
          </table>
        </div>

        <div class="section">
          <h2>Summary</h2>

          <div class="summary-row">
            <span class="label">Subtotal:</span>
            <span class="summary-value">
              <xsl:value-of select="inv:Summary/inv:Subtotal"/>
            </span>
          </div>

          <div class="summary-row">
            <span class="label">Tax:</span>
            <span class="summary-value">
              <xsl:value-of select="inv:Summary/inv:Tax"/>
            </span>
          </div>

          <div class="summary-row">
            <span class="label">Total Due:</span>
            <span class="summary-value">
              <xsl:value-of select="inv:Summary/inv:TotalDue"/>
            </span>
          </div>
        </div>

        <div class="section">
          <h2>Notes</h2>
          <p><xsl:value-of select="inv:Notes"/></p>
        </div>

      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
