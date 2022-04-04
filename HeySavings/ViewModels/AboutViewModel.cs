using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using HeySavings.SQL_Lite;
using HeySavings.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HeySavings.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public int id { get; set; }

        string _budget;
        public string budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value); }
        }


        int _index;
        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        bool _isLoading;
        public bool isLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public AboutViewModel()
        {
            Index = -1;
            isLoading = false;

        }

        public ICommand FirstPageCommand => new Command(() =>
        {
            App.Current.MainPage = new AboutPage1(); 
        });

        public ICommand SecondPageCommand => new Command(() =>
        {
            App.Current.MainPage = new AboutPage2();
        });

        public ICommand ThriPageCommand => new Command(async() =>
        {
            if (Index < 0)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Select Currency type!", new TimeSpan(2));
                return;
            }
            if (string.IsNullOrEmpty(this.budget))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter Budget!", new TimeSpan(2));
                return;
            }

            string currency = (Index == 0) ? "RON" : "EURO";
            string symbol = (Index == 0) ? "LEI" : "€";
            Budget budget = new Budget()
            {
                id = App.login.id,
                budget = this.budget,
                Currency = currency,
                CurrencySymbol = symbol
            };
            App.Database.SaveBudget(budget);
            Acr.UserDialogs.UserDialogs.Instance.Toast("Welcome " + App.login.firstname, new TimeSpan(3));
            isLoading = true;
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage = new AppShell();
                });
            });
        });
    }
}
