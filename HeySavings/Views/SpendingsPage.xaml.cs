using System;
using System.Collections.Generic;
using HeySavings.ViewModels;
using Xamarin.Forms;

namespace HeySavings.Views
{
    public partial class SpendingsPage : ContentPage
    {
        public SpendingsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            SpendingsPageViewModel vm = (SpendingsPageViewModel)this.BindingContext;
            vm.LoadList();
        }
    }
}
