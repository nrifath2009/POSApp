using POSApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace POSApp.ViewModels.Products
{
    public class NewProductViewModel : BaseViewModel<Product>
    {        
        private string productName;
        private double price;
        private int quantity;
        private string productUnit;

        public NewProductViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(productName)
                && !String.IsNullOrWhiteSpace(price.ToString());
        }

        public string ProductName
        {
            get => productName;
            set => SetProperty(ref productName, value);
        }

        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }
        public string ProductUnit
        {
            get => productUnit;
            set => SetProperty(ref productUnit, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Product newItem = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                ProductName = ProductName,
                Quantity = Quantity,
                Price = Price,
                ProductUnit = ProductUnit
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    
    }
}
