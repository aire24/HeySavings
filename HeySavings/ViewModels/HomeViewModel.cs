using System;
using System.Collections.Generic;
using System.Linq;
using HeySavings.Popups;
using HeySavings.SQL_Lite;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HeySavings.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        IPopupNavigation _popup { get; set; }
        string _totalBudget;
        public string Totalbudget
        {
            get { return _totalBudget; }
            set { SetProperty(ref _totalBudget, value); }
        }

        string _Remainingbudget;
        public string Remainingbudget
        {
            get { return _Remainingbudget; }
            set { SetProperty(ref _Remainingbudget, value); }
        }
        

        public HomeViewModel()
        {
            
            _popup = PopupNavigation.Instance;
            
        }


        public void LoadData()
        {
            Budget b = App.Database.GetItem(App.login.id);

            double totalbudget = Convert.ToDouble(b.budget);
            Totalbudget = totalbudget.ToString("0.00") + "  ( " + b.CurrencySymbol + " )";


            List<Spendings> lst = App.Database.getAllSpendings(App.login.id);

            Remainingbudget = (totalbudget - (lst.Sum(x => Convert.ToDouble(x.amount)))).ToString("0.00") + "  ( " + b.CurrencySymbol + " )";

        }
        public Command pieClicked => new Command(() =>
         {
             _popup.PushAsync(new PopupOpenType(), true);

         });

    }
}
