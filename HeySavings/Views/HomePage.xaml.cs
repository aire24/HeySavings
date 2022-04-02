using System;
using System.Collections.Generic;
using HeySavings.Services;
using HeySavings.ViewModels;
using Xamarin.Forms;

namespace HeySavings.Views
{
    public partial class HomePage : BasePage
    {
        HomeViewModel vm;
        public HomePage()
        {
            InitializeComponent();
            vm = new HomeViewModel();
            this.BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            vm.LoadData();
        }
    }
}
