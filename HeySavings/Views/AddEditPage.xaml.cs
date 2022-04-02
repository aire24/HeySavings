using System;
using System.Collections.Generic;
using HeySavings.Enums;
using HeySavings.SQL_Lite;
using HeySavings.ViewModels;
using Xamarin.Forms;

namespace HeySavings.Views
{
    public partial class AddEditPage : ContentPage
    {
        public AddEditPage(SpendingType type, double remainingbudget)
        {
            InitializeComponent();
            this.BindingContext = new AddEditPageViewModel(type, remainingbudget);
        }

        public AddEditPage(Spendings edit, double remainingbudget)
        {
            InitializeComponent();
            this.BindingContext = new AddEditPageViewModel(edit, remainingbudget);

        }
    }
}
