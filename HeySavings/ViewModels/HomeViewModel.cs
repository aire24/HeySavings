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

        string _BudgetExplain;
        public string BudgetExplain
        {
            get { return _BudgetExplain; }
            set { SetProperty(ref _BudgetExplain, value); }
        }


        bool _showEditBudget;
        public bool showEditBudget
        {
            get { return _showEditBudget; }
            set { SetProperty(ref _showEditBudget, value); }
        }
        public HomeViewModel()
        {
            
            _popup = PopupNavigation.Instance;
            showEditBudget = false;


        }

        Budget bdgt;
        public void LoadData()
        {
            //App.Database.createMonthlyBudget(true);
            MonthlyBudget b = App.Database.getMonthlyBudget(App.login.id);
            if (b == null)
            {
                App.Database.createMonthlyBudget();
                b = App.Database.getMonthlyBudget(App.login.id);
            }
            bdgt = App.Database.getBudget(App.login.id);
            double totalbudget = Convert.ToDouble(b.monthlybudget);
            try
            {
                double diff = totalbudget - Convert.ToDouble(b.lastMonthRemaingBudget);
                double monthlybdgt = Convert.ToDouble(bdgt.budget);
                if (diff != monthlybdgt)
                {
                    totalbudget = monthlybdgt + Convert.ToDouble(b.lastMonthRemaingBudget);
                }
            }
            catch (Exception EX)
            {

            }

            Totalbudget = totalbudget.ToString("0.00") + "  ( " + bdgt.CurrencySymbol + " )";
            if (!string.IsNullOrEmpty(b.lastMonthRemaingBudget) && Convert.ToDouble(b.lastMonthRemaingBudget) > 0)
                BudgetExplain = "(" + b.lastMonthRemaingBudget + "+" + (totalbudget - Convert.ToDouble(b.lastMonthRemaingBudget)) + ")"; 

            List<Spendings> lst = App.Database.getMonthAllSpendings(App.login.id, b.id);
            double tempremain = (totalbudget - lst.Sum(x => Convert.ToDouble(x.amount)))- ((totalbudget * 20) / 100);
            
            Remainingbudget = tempremain + "  ( " + bdgt.CurrencySymbol + " )";

        }
        public Command pieClicked => new Command(() =>
         {
             _popup.PushAsync(new PopupOpenType(), true);

         });

        public Command hideEdit => new Command(() =>
         {
             showEditBudget = false;
         });

        public Command editBudget => new Command(() =>
        {
            showEditBudget = true;

        });

        double _newBudget;
        public double newBudget
        {
            get { return _newBudget; }
            set {
                SetProperty(ref _newBudget, value);
            }
        }

        public Command ResetBudget => new Command(() =>
        {
            try
            {
                bdgt.budget = newBudget.ToString("0.00");
                App.Database.UpdateBudget(bdgt);
                Acr.UserDialogs.UserDialogs.Instance.Toast("Budget was Updated Successfully!", new TimeSpan(2));
                LoadData();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Error While Updating!", new TimeSpan(2));
            }
            finally {
                showEditBudget = false;
            }


      
    }); 

    }
}
