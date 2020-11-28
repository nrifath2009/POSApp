using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using POSApp.Services;
using POSApp.Views;

namespace POSApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ProductDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
