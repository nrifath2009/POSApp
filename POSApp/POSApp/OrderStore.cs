using POSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSApp
{
    public class OrderStore
    {
        private static List<Order> _orders = new List<Order>();
        
        public static void AddToOrderStore(Order order)
        {
            _orders.Add(order);
        }

        public static List<Order> GetFromOrderStore()
        {
            return _orders.ToList();
        }
    }
}
