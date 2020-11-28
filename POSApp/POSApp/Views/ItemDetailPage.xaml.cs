using System.ComponentModel;
using Xamarin.Forms;
using POSApp.ViewModels;

namespace POSApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}