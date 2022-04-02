using System;
using Android.Graphics.Drawables;
using HeySavings.CustomControls;
using HeySavings.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryBlack), typeof(EntryBlackRendrer))]

namespace HeySavings.Droid.CustomControls
{
    public class EntryBlackRendrer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}
