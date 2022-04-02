using Acr.UserDialogs;
using HeySavings.SQL_Lite;
using HeySavings.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeySavings.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        bool _showPassword;
        public bool ShowPassword
        {
            get { return _showPassword; }
            set { SetProperty(ref _showPassword, value); }
        }


        
        bool _isLoading;
        public bool isLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }


        string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

     


        public LoginViewModel()
        {
            Email = App.login.email;
            ShowPassword = true;
            isLoading = false;
           

        }


        public Command clickEye => new Command(() =>
         {
             //string image = (ShowPassword) ? "blind.png" : "viewer.png";
             //eyeIcon = new BitmapImage(new Uri(image, UriKind.Relative));
             
             ShowPassword = !ShowPassword;

         });


        public Command LoginCommand => new Command(async ()=>
        {
            if (string.IsNullOrEmpty(email))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter email id!", new TimeSpan(1));
                return;
            }

            if(!ValidateEmail(Email))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter Valid email id!", new TimeSpan(1));
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter password!", new TimeSpan(1));
                return;
            }

            try
            {
                isLoading = true;
                Email = Email.Trim();

                await Task.Run(() =>
                {
                    LoginTable userDetail = App.Database.GetItem(email.TrimEnd().TrimStart(), password);

                    if (userDetail != null)
                    {
                        if (email != userDetail.email && password != userDetail.password)
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid credential! try again", new TimeSpan(2));
                        }
                        else
                        {
                            App.login = userDetail;

                            Budget budget = App.Database.GetItem(userDetail.id);
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (budget == null)
                                {
                                    App.Current.MainPage = new AboutPage();

                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Toast("Welcome " + userDetail.firstname, new TimeSpan(2));
                                    App.Current.MainPage = new AppShell();

                                }
                            });



                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Login failed! try again", new TimeSpan(2));
                        isLoading = true;

                    }

                });
            }
            finally
            {
                isLoading = false;
            }

        });

        public Command RegisterCommand => new Command(() =>
        {
            App.Current.MainPage = new RegisterPage();
        });
    }
}
