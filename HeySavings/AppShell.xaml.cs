using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HeySavings.ViewModels;
using HeySavings.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HeySavings
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            UserDialogs.Instance.HideLoading();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        long lastPress;

        async void OpenNewShell()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage = new AppShell();
                });
            });
        }

        protected override bool OnBackButtonPressed()
        {
            Page pg = ((AppShell)App.Current.MainPage).CurrentPage;
            if (pg is Views.SpendingDetailPage)
            {
                SpendingDetailPageViewModel vm = (SpendingDetailPageViewModel)pg.BindingContext;
                vm.isLoading = true;
                OpenNewShell();
                return true;
            }
            
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                PopupNavigation.Instance.PopAllAsync();
                return true;
            }

            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            if (currentTime - lastPress > 5000)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Press back again to exit!", new TimeSpan(1));
                lastPress = currentTime;
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }

    
    }
}
