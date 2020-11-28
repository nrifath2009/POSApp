using System;
using System.Collections.Generic;
using POSApp.ViewModels;
using POSApp.Views;
using POSApp.Views.Products;
using Xamarin.Forms;

namespace POSApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
            Routing.RegisterRoute(nameof(NewProductPage), typeof(NewProductPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
