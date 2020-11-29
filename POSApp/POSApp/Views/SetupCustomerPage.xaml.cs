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
    public partial class SetupCustomerPage : ContentPage
    {
        public SetupCustomerPage()
        {
            InitializeComponent();
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.CustomerId =  CustomerIdEntryBox.Text;
            customerInfo.FamilySize = int.Parse(FamilySizeEntryBox.Text);
            customerInfo.VoucherLimit = double.Parse(VoucherLimitEntryBox.Text);
            AppStore.AddCustomerInfo(customerInfo);
            await Navigation.PopModalAsync();
        }

        private async void closeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}