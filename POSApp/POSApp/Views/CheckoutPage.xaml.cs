using POSApp.Models;
using POSApp.ViewModels;
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
    public partial class CheckoutPage : ContentPage
    {
        public VoucherProductsViewModel _viewModel { get; set; }
        
        public CheckoutPage()
        {
            InitializeComponent();
            BindingContext =  _viewModel = new VoucherProductsViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadOrdersFromStore();
            var html = new HtmlWebViewSource
            {
                Html = InvoiceGenerator.GetInvoice(_viewModel.Orders)
            };
            InvoiceWebView.Source = html;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            PrintOut();
        }

        private void PrintOut()
        {
            var printService = DependencyService.Get<IPrintService>();
            printService.Print(InvoiceWebView);
            //printService.PrintText(InvoiceGenerator.GetInvoice());
        }
    }
}