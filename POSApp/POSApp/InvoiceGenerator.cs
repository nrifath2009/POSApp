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
                tBodyContent += $"<tr><td>{item.ProductNameWithUnit}</td><td>{item.Quantity} {item.ProductUnit}</td><td>{item.CalculatedPrice}</td></tr>";
                totalAmount += (int.Parse(item.Quantity)*item.Price);
            }

            string totalValue = $"<tr><td>Total: </td><td></td><td>{totalAmount} BDT</td></tr>";
            tBodyContent += totalValue;

            text = text.Replace("{{TBODY}}", tBodyContent);
            return text;
        }
    }
}
