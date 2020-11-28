using POSApp.Models;
using POSApp.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POSApp.Views.Products
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : ContentPage
    {
        public Product Item { get; set; }
        public NewProductPage()
        {
            InitializeComponent();
            BindingContext = new NewProductViewModel();
        }
    }
}