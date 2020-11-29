using POSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSApp
{
    public class AppStore
    {
        private static List<Order> _orders = new List<Order>();
        private static CustomerInfo _customerInfo = new CustomerInfo();
        
        public static void AddToOrderStore(Order order)
        {
            _orders.Add(order);
        }

        public static List<Order> GetFromOrderStore()
        {
            return _orders.ToList();
        }

        public static void ResetAfterPrint()
        {
            _orders = new List<Order>();
            _customerInfo = new CustomerInfo();
        }

        public static void AddCustomerInfo(CustomerInfo customerInfo)
        {
            _customerInfo = customerInfo;
        }
        public static CustomerInfo GetCustomerInfo()
        {
            return _customerInfo;
        }
    }
}
