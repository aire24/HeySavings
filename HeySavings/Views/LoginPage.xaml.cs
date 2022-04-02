﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeySavings.Services;
using HeySavings.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeySavings.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        
    }
}
