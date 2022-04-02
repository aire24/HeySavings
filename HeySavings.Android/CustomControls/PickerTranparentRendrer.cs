using System;
using Android.Graphics.Drawables;
using HeySavings.CustomControls;
using HeySavings.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PickerTransparent), typeof(PickerTranparentRendrer))]

namespace HeySavings.Droid.CustomControls
{
    public class PickerTranparentRendrer : PickerRenderer
    {
       
            protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
            {
                base.OnElementChanged(e);
                if (Control != null)
                {
                    Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                }
            }
        
    }
}
