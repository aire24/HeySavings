using System;
using System.Collections.Generic;
using HeySavings.Enums;
using HeySavings.ViewModels;
using Xamarin.Forms;

namespace HeySavings.Views
{
    public partial class SpendingDetailPage : ContentPage
    {
        SpendingDetailPageViewModel vm;
        public SpendingDetailPage(SpendingType type)
        {
            InitializeComponent();
            vm = new SpendingDetailPageViewModel(type);
            this.BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            vm.LoadList();
        }

    }
}
