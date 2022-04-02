using System;
using HeySavings.SQL_Lite;
using Xamarin.Forms;

namespace HeySavings.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        string _budget;
        public string Budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value); }
        }


        string firstname;
        public string FirstName
        {
            get { return firstname; }
            set { SetProperty(ref firstname, value); }
        }

        string lastname;
        public string LastName
        {
            get { return lastname; }
            set { SetProperty(ref lastname, value); }
        }
        Budget b;

        public ProfileViewModel()
        {
            b = App.Database.GetItem(App.login.id);
            FirstName = App.login.firstname;
            LastName = App.login.lastname;
            Budget = b.budget;
            
        }


        public Command UpdateProfile => new Command(() =>
        {
            try
            {
                b.budget = this.Budget;
                App.Database.UpdateBudget(b);

                LoginTable userdetail = App.login;
                userdetail.firstname = FirstName;
                userdetail.lastname = LastName;

                App.Database.SaveItem(userdetail);

                Acr.UserDialogs.UserDialogs.Instance.Toast("Profile Saved Successfully!", new TimeSpan(2));

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Error While Saving!", new TimeSpan(2));

            }


        });
    }
}
