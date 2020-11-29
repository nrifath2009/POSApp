using POSApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace POSApp
{
    public class InvoiceGenerator
    {
        public static string GetInvoice(List<Order> orders)
        {
            string invoiceString = "<p>Test Invoice Print For Today</p>";
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(InvoiceGenerator)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("POSApp.invoice.html");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var customerInfo =  AppStore.GetCustomerInfo();

            text = text.Replace("{{IDNO}}",customerInfo.CustomerId);
            text = text.Replace("{{FAMILYSIZE}}", customerInfo.FamilySize.ToString());
            text = text.Replace("{{VOUCHERLIMIT}}", customerInfo.VoucherLimit.ToString());

            string tBodyContent = string.Empty;
            decimal totalAmount = 0;
            foreach (var item in orders)
            {
                tBodyContent += $"<tr><td  style='font-weight:bold;border-bottom:2px dotted;'>{item.ProductNameWithUnit}</td><td style='border-bottom:2px dotted;'>{item.Quantity} {item.ProductUnit}</td><td style='border-bottom:2px dotted;'>{item.CalculatedPrice}</td></tr>";
                totalAmount += (int.Parse(item.Quantity)*item.Price);
            }

            string totalValue = $"<tr><td>Total: </td><td></td><td>{totalAmount}</td></tr>";
            tBodyContent += totalValue;

            string orderDate = $"<tr><td>Date: </td><td></td><td>{DateTime.Now.Date.ToString("dd-MM-yyyy")}</td></tr>";
            string orderTime = $"<tr><td>Time: </td><td></td><td>{DateTime.Now.ToShortTimeString()}</td></tr>";

            string spaces = @"<p class='section-width' style='border-bottom: 2px dotted;'></p>";

            tBodyContent += spaces;
            tBodyContent += orderDate;
            tBodyContent += orderTime;

            text = text.Replace("{{TBODY}}", tBodyContent);
            return text;
        }
    }
}
