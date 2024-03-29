﻿using POSApp.Models;
using POSApp.ViewModels.Products;
using POSApp.Views;
using POSApp.Views.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace POSApp.ViewModels
{
    public class VoucherProductsViewModel : BaseViewModel<Product>
    {
        private Product _selectedItem;

        public ObservableCollection<Product> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SetupCustomerCommand { get; }
        public Command PrintCommand { get; }
        public Command<Product> ItemTapped { get; }

        public List<Order> Orders { get; set; }

        public VoucherProductsViewModel()
        {
            Title = "Product List";
            Items = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Product>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            SetupCustomerCommand = new Command(OnSetupCustomerCommand);
            LoadOrdersFromStore();
        }
        public async void OnSetupCustomerCommand()
        {
            await Shell.Current.GoToAsync(nameof(SetupCustomerPage));
        }
        public void LoadOrdersFromStore()
        {
            Orders = AppStore.GetFromOrderStore() ?? new List<Order>();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            await ExecuteLoadItemsCommand();
            LoadOrdersFromStore();
        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewProductPage));
        }

        async void OnItemSelected(Product item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailViewModel.ItemId)}={item.Id}");
        }
        public void AddToOrderStore(Order order)
        {
            AppStore.AddToOrderStore(order);
        }

        
    }
}
