using POSApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace POSApp.ViewModels
{
    public class NewOrderViewModel : BaseViewModel<Order>
    {
        private string productName;
        private string productId;
        private decimal price;
        private string quantity;
        private string customerCardNumber;

        private List<Order> orders;

        public NewOrderViewModel()
        {
            orders = new List<Order>();
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            AddToOrderListCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(quantity)
                && !String.IsNullOrWhiteSpace(customerCardNumber);
        }

        public string ProductName
        {
            get => productName;
            set => SetProperty(ref productName, value);
        }

        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public string ProductId
        {
            get => productId;
            set => SetProperty(ref productId, value);
        }

        public string Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }
        public string CustomerCardNumber
        {
            get => customerCardNumber;
            set => SetProperty(ref customerCardNumber, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public Command AddToOrderListCommand { get; }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Order newItem = new Order()
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = ProductId,
                ProductName = ProductName,
                Quantity = Quantity,
                Price = Price
            };

            orders.Add(newItem);
            // DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
