using POSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POSApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        private Product productSelected;
        public string ProductQuantity { get; set; }
        public OrderPage(Product product)
        {
            InitializeComponent();
            this.productSelected = product;
            BindingContext = this.productSelected;
        }

        private async void closeProductCartModal_Clicked(object sender, EventArgs e)
        {
            //ProductQuantity =  QuantityEntryBox.Text;
            await Navigation.PopModalAsync();
        }

        private async void addToItemButton_Clicked(object sender, EventArgs e)
        {
            ProductQuantity = QuantityEntryBox.Text;
            var productQuantity = int.Parse(ProductQuantity);
            var order = new Order
            {
                Price = productSelected.Price,
                ProductName = productSelected.ProductName,
                ProductId = productSelected.Id,
                OrderDate = DateTime.Now,
                Quantity = productQuantity,
                ProductUnit = productSelected.ProductUnit
            };

            var customerInfo =  AppStore.GetCustomerInfo();
            var customerOrders = AppStore.GetFromOrderStore();
            if (customerInfo != null)
            {
                if (customerOrders != null)
                {
                    var customerTotal = customerOrders.Sum(c => c.Quantity * c.Price);
                    var limitLeft = customerInfo.VoucherLimit - customerTotal;
                    if(customerTotal+(order.Price*order.Quantity) > customerInfo.VoucherLimit)
                    {
                        await DisplayAlert("Voucher Limit Reached", $"Voucher Limit {customerInfo.VoucherLimit} will be cross if add this item. you can add more up to {limitLeft} BDT","OK");
                        return;
                    }
                }
            }
            var productExist = AppStore.GetFromOrderStore();
            if(productExist!=null && productExist.Any(c => c.ProductId == order.ProductId))
            {
                var oldItem = productExist.FirstOrDefault(c => c.ProductId == order.ProductId);
                if (oldItem != null)
                {
                    oldItem.Quantity += order.Quantity;
                }
            }
            else
            {
                AppStore.AddToOrderStore(order);
            }
            
            await Navigation.PopModalAsync();
        }
    }
}