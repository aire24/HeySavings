using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HeySavings.Enums;
using HeySavings.SQL_Lite;
using System.Linq;
using Xamarin.Forms;
using HeySavings.Views;

namespace HeySavings.ViewModels
{
    public class SpendingDetailPageViewModel : BaseViewModel
    {
        Budget _b;
        public Budget b
        {
            get { return _b; }
            set { SetProperty(ref _b, value); }
        }
        SpendingType typeSpending;
        
        bool _isLoading;
        public bool isLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        public SpendingDetailPageViewModel(SpendingType type)
        {
            typeSpending = type;
        }
        string heading;
        public string Heading
        {
            get { return heading; }
            set { SetProperty(ref heading, value); }
        }
        double rembudget;
        public double RemBudget
        {
            get { return rembudget; }
            set { SetProperty(ref rembudget, value); }
        }
        double totalbudget;
        public double TotalBudget
        {
            get { return totalbudget; }
            set { SetProperty(ref totalbudget, value); }
        }
        ObservableCollection<Spendings> lstSpendings;
        public ObservableCollection<Spendings> LstSpendings
        {
            get { return lstSpendings; }
            set { SetProperty(ref lstSpendings, value); }
        }
        public void LoadList()
        {
            var data = App.Database.getAllSpendings(App.login.id);
            List<Spendings> temp = App.Database.getAllSpendings(App.login.id).Where(x=> x.type == typeSpending).ToList();
            MonthlyBudget bdgt = App.Database.getMonthlyBudget(App.login.id);
            b = App.Database.getBudget(App.login.id);
            double totalbudget = Convert.ToDouble(bdgt.monthlybudget);
            if (temp != null)
            {
                LstSpendings = new ObservableCollection<Spendings>(temp);
                if (typeSpending == SpendingType.Needs)
                {
                    Heading = "Needs Budget";
                    TotalBudget = (totalbudget / 100) * 50;
                }
                else if (typeSpending == SpendingType.Wants)
                {
                    Heading = "Wants Budget";
                    TotalBudget = (totalbudget / 100) * 30;
                }
                else {
                    Heading = "Savings Budget";
                    TotalBudget = (totalbudget / 100) * 20;
                }

                double totalSpent = LstSpendings.Sum(x => Convert.ToDouble(x.amount));
                RemBudget = (TotalBudget - totalSpent);
            }
        }
        public Command AddNew => new Command(() =>
         {
             Navigation.PushAsync(new AddEditPage(typeSpending, RemBudget));
         });
        public Command<Spendings> Edit => new Command<Spendings>((spending) =>
        {
            Navigation.PushAsync(new AddEditPage(spending, RemBudget));
        });
        public Command<Spendings> Delete => new Command<Spendings>(async(spending) =>
        {
          bool res =  await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Are you sure. You want to Delete?", okText:"Delete", cancelText: "No");
            if (res)
            {
                int result = App.Database.deleteSpending(spending);
                if (result == 1)
                {
                    LoadList();
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Deleted Successfully!", TimeSpan.FromMilliseconds(100));
                }
            }
        });
    }
}
