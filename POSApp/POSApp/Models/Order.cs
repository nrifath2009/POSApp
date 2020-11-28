using System;
using System.Collections.Generic;
using System.Text;

namespace POSApp.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string CustomerCardNumber { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public DateTime OrderDate { get; set; }




    }
}
