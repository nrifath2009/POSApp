﻿using POSApp.Models;
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
        }
    }
}