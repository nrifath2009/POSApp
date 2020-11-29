using POSApp.Models;
using POSApp.ViewModels;
using POSApp.ViewModels.Products;
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
    public partial class NewOrderPage : ContentPage
    {
        VoucherProductsViewModel _viewModel;
        public NewOrderPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new VoucherProductsViewModel();
        }

        private void LoadGridData(int noOfProducts)
        {
            Grid productGrid = new Grid
            {
                Margin = 15,                
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };

            RowDefinitionCollection rows = new RowDefinitionCollection();

            for (int i = 0; i <= _viewModel.Items.Count/2; i++)
            {
                rows.Add(new RowDefinition
                {
                    Height = new GridLength(150)
                });
            }
            productGrid.RowDefinitions = rows;
            int rowCount = 0;
            int colCount = 0;
            int count = 0;
            int itemLooks = 0;
            foreach (var item in _viewModel.Items)
            {
                var button = new Button
                {
                    BackgroundColor = Color.FromHex(AppConstant.PrimaryColor),                    
                    Text = item.ProductName + Environment.NewLine + "Price: "+item.Price+Environment.NewLine+"Stock: "+item.Quantity,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BindingContext = item
                };
                button.Clicked += OnProductSelectButton_Clicked;
                //button.Command = new Command<Product>(OnProductSelectButton_Clicked);
                productGrid.Children.Add(button,colCount, rowCount);
                //productGrid.Children.Add(new Label
                //{
                //    TextColor = Color.White,
                //    Text = item.ProductName+Environment.NewLine+item.Price,
                //    FontSize = 18,
                //    HorizontalOptions = LayoutOptions.Center,
                //    VerticalOptions = LayoutOptions.Center

                //}, colCount, rowCount);

                count++;
                itemLooks++;

                if (count % 2 == 0)
                {
                    rowCount++;
                    colCount = 0;
                }
                if(count %2 != 0)
                {
                    colCount++;
                }

            }
            
            productGridScroller.Content = productGrid;
        }

        private async void OnProductSelectButton_Clicked(object sender, EventArgs e)
        {
            var selectedProduct  =  sender as Button;
            if (selectedProduct != null)
            {
                var productSelected =  selectedProduct.BindingContext as Product;
                var orderPage = new OrderPage(productSelected);
                await Navigation.PushModalAsync(orderPage);
                
                //await DisplayAlert("Selected Product", productSelected.ProductName,"Close");
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            LoadGridData(16);
        }

        

        private async void setupToolBar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SetupCustomerPage());
        }
    }
}