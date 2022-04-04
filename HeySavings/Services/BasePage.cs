using System;
using Xamarin.Forms;

namespace HeySavings.Services
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        long lastPress;
        protected override bool OnBackButtonPressed()
        {
            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            if (currentTime - lastPress > 5000)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Press back again to exit!", new TimeSpan(1));
                lastPress = currentTime;
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}
