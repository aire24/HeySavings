using System;
using HeySavings.Enums;
using HeySavings.ViewModels;
using HeySavings.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HeySavings.Popups
{
    public class PopupPageViewModel : BaseViewModel
    {
        public PopupPageViewModel()
        {
        }


        public Command<string> OpenNextPage => new Command<string>((args) =>
         {

             PopupNavigation.Instance.PopAllAsync();
            
             if (args == "Savings")     Navigation.PushAsync(new SpendingDetailPage(SpendingType.Savings));
             else if (args == "Needs")  Navigation.PushAsync(new SpendingDetailPage(SpendingType.Needs));
             else   Navigation.PushAsync(new SpendingDetailPage(SpendingType.Wants));

         });

       
    }
}
