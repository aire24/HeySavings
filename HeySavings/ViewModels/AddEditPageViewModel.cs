using System;
using HeySavings.Enums;
using HeySavings.SQL_Lite;
using Xamarin.Forms;

namespace HeySavings.ViewModels
{
    public class AddEditPageViewModel : BaseViewModel
    {
        double Remainingbudget;
        SpendingType type;
        Spendings sp;
        string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }


        bool isEdit;
        public bool IsEdit
        {
            get { return isEdit; }
            set { SetProperty(ref isEdit, value); }
        }

        string amount;
        public string Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }


        public AddEditPageViewModel(SpendingType sptype, double remainingbudget)
        {
            IsEdit = false;
            type = sptype;
            Remainingbudget = remainingbudget;
        }

        public AddEditPageViewModel(Spendings spending, double remainingbudget)
        {
            IsEdit = true;
            sp = spending;
            Name = sp.spendingName;
            Amount = sp.amount;
            Description = sp.spendingDescription;
            Remainingbudget = remainingbudget;
        }

        public Command SaveUpdate => new Command(() =>
        {
            double amount = Convert.ToDouble(Amount);
            if (string.IsNullOrEmpty(Amount) || amount <= 0)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid amount!", new TimeSpan(1));
                return;
            }

            if (amount > Remainingbudget)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Your budget is too low to spend!", new TimeSpan(1));
                return;
            }

          
            int result = 0;
            if (isEdit == false)
            {
                sp = new Spendings()
                {
                    amount = Amount,
                    id = App.login.id,
                    spendingDescription = Description,
                    spendingName = Name,
                    type = this.type
                };
                result = App.Database.AddSpendng(sp);

                 Acr.UserDialogs.UserDialogs.Instance.Toast((result > 0)?"Added Successfully!" : "Error While Saving!", new TimeSpan(1));
                

            }
            else
            {
                sp.amount = Amount;
                sp.spendingDescription = Description;
                sp.spendingName = Name;
                result = App.Database.UpdateSpending(sp);
                Acr.UserDialogs.UserDialogs.Instance.Toast((result > 0) ? "Updated Successfully!" : "Error While Updating!", new TimeSpan(1));

            }

            if (result != 0) Navigation.PopAsync();

        });


    }
}
