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
            var productQuantity = ProductQuantity;
            var order = new Order
            {
                Price = productSelected.Price,
                ProductName = productSelected.ProductName,
                ProductId = productSelected.Id,
                OrderDate = DateTime.Now,
                Quantity = productQuantity
            };
            AppStore.AddToOrderStore(order);
            await Navigation.PopModalAsync();
        }
    }
}