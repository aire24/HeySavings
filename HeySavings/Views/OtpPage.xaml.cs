using System;
using System.Collections.Generic;
using HeySavings.ViewModels;
using Xamarin.Forms;

namespace HeySavings.Views
{
    public partial class OtpPage : ContentPage
    {
        public OtpPage(string otp, string emailid)
        {
            InitializeComponent();
            BindingContext = new OtpPageViewModel(otp, emailid);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OtpEntry.Focus();


        }

        void OnOtpEntryFocus(System.Object sender, System.EventArgs e)
        {
            OtpEntry.Focus();
        }
    }
}
