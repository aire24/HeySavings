using System;
using HeySavings.SQL_Lite;
using HeySavings.Views;
using Xamarin.Forms;

namespace HeySavings.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
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
        bool _showPassword;
        public bool ShowPassword
        {
            get { return _showPassword; }
            set { SetProperty(ref _showPassword, value); }
        }
        public RegisterViewModel()
        {
            ShowPassword = true;
        }
        public Command GoBack => new Command(() =>
        {
            App.Current.MainPage = new LoginPage();
        });
        public Command clickEye => new Command(() =>
        {
            //string image = (ShowPassword) ? "blind.png" : "viewer.png";
            //eyeIcon = new BitmapImage(new Uri(image, UriKind.Relative));
            ShowPassword = !ShowPassword;
        });
        public Command Register => new Command(() =>
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter First Name!", new TimeSpan(2));
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter Last Name!", new TimeSpan(2));
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter email id!", new TimeSpan(2));
                return;
            }
            if (!ValidateEmail(Email))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter Valid email id!", new TimeSpan(2));
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter password!", new TimeSpan(2));
                return;
            }
            if (password.Length<4)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Password must be atleast 4 characters!", new TimeSpan(2));
                return;
            }
            LoginTable register = new LoginTable()
            {
                email = Email,
                password = Password,
                firstname = FirstName,
                lastname = LastName
            };
            int i = App.Database.SaveItem(register);
            if (i > 0)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Register Successfully!", new TimeSpan(2));
                App.login = register;
                App.Current.MainPage = new LoginPage();
            }
            else
            {
            }
        });
    }
}
