using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace POSApp.Models
{
    public class Product: RealmObject
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }



    }
}
