using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeySavings.SQL_Lite;

namespace HeySavings.ViewModels
{
    public class SpendingsPageViewModel : BaseViewModel
    {
        ObservableCollection<Spendings> lstSavings;
        public ObservableCollection<Spendings> LstSavings
        {
            get { return lstSavings; }
            set { SetProperty(ref lstSavings, value); }
        }


        ObservableCollection<Spendings> lstNeeds;
        public ObservableCollection<Spendings> LstNeeds
        {
            get { return lstNeeds; }
            set { SetProperty(ref lstNeeds, value); }
        }

        ObservableCollection<string> _Years;
        public ObservableCollection<string> Years
        {
            get { return _Years; }
            set { SetProperty(ref _Years, value); }
        }

        ObservableCollection<Spendings> lstWants;
        public ObservableCollection<Spendings> LstWants
        {
            get { return lstWants; }
            set { SetProperty(ref lstWants, value); }
        }

        string _SelectedYear;
        public string SelectedYear
        {
            get { return _SelectedYear; }
            set { SetProperty(ref _SelectedYear, value);
                LoadList(); }
        }

        int _SelectedMonth;

        public int SelectedMonth
        {
            get { return _SelectedMonth; }
            set { SetProperty(ref _SelectedMonth, value);
                LoadList(); }
        }
        public SpendingsPageViewModel()
        {
            DateTime now = DateTime.Now;
            Years = new ObservableCollection<string>(Enumerable.Range(2021, (now.Year - 2021)+1).Select(x=>x.ToString()));
            SelectedYear = now.Year.ToString();
            SelectedMonth = now.Month-1;
            loaded = true;
            //LoadList();
        }

        string totalbudget;
        public string TotalBudget
        {
            get { return totalbudget; }
            set { SetProperty(ref totalbudget, value); }
        }


        bool loaded = false;
        public void LoadList()
        {
            try
            {
                if (loaded == false) return;
                var b = App.Database.getPreviousMonthlyBudget(new DateTime(Convert.ToInt16(SelectedYear), SelectedMonth + 1, 1));
                var budget = App.Database.getMonthlyBudget(App.login.id);
                var bdgt = App.Database.getBudget(App.login.id);
                double totalbudget = Convert.ToDouble(b.monthlybudget);
                try
                {
                    double diff = totalbudget - Convert.ToDouble(b.lastMonthRemaingBudget);
                    double monthlybdgt = Convert.ToDouble(bdgt.budget);

                    TotalBudget = (monthlybdgt + Convert.ToDouble(b.lastMonthRemaingBudget)).ToString("0.00") + "  ( " + bdgt.CurrencySymbol + " )";
                }
                catch (Exception ex)
                { }
                if (b == null)
                {
                    LstNeeds.Clear();
                    LstWants.Clear();
                    LstSavings.Clear();
                    return;
                }
                var data = App.Database.getAllSpendings(App.login.id);
                List<Spendings> temp = App.Database.getAllSpendings(App.login.id).Where(x => x.MonthlybudgetId == b.id).ToList();
                if (temp != null)
                {
                    LstNeeds = new ObservableCollection<Spendings>(temp.Where(x => x.type == Enums.SpendingType.Needs).ToList());
                    LstWants = new ObservableCollection<Spendings>(temp.Where(x => x.type == Enums.SpendingType.Wants).ToList());
                    LstSavings = new ObservableCollection<Spendings>(temp.Where(x => x.type == Enums.SpendingType.Savings).ToList());

                }
            }
            catch (Exception ex)
            {
                TotalBudget = "0.00";
            }
        }
    }
}
