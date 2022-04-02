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


        ObservableCollection<Spendings> lstWants;
        public ObservableCollection<Spendings> LstWants
        {
            get { return lstWants; }
            set { SetProperty(ref lstWants, value); }
        }

        public SpendingsPageViewModel()
        {
            LoadList();
        }

        public void LoadList()
        {
            var data = App.Database.getAllSpendings(App.login.id);
            List<Spendings> temp = App.Database.getAllSpendings(App.login.id).ToList();
            if (temp != null)
            {
                LstNeeds = new ObservableCollection<Spendings>(temp.Where(x => x.type == Enums.SpendingType.Needs).ToList());
                LstWants = new ObservableCollection<Spendings>(temp.Where(x => x.type == Enums.SpendingType.Wants).ToList());
                LstSavings = new ObservableCollection<Spendings>(temp.Where(x => x.type == Enums.SpendingType.Savings).ToList());

            }
        }
    }
}
