using System;
using System.Collections.Generic;
using HeySavings.Services;
using HeySavings.ViewModels;
using Xamarin.Forms;

namespace HeySavings.Views
{
    public partial class ProfilePage : BasePage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ProfileViewModel vm = (ProfileViewModel)this.BindingContext;
            vm.Load();
        }
    }
}
