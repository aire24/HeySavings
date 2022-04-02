using System;
using System.Collections.Generic;
using HeySavings.Services;
using HeySavings.ViewModels;
using Xamarin.Forms;

namespace HeySavings.Views
{
    public partial class RegisterPage : BasePage
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterViewModel();

        }


        
    }
}
