using System;
using Android.Graphics.Drawables;
using HeySavings.CustomControls;
using HeySavings.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(BorderLessEditor), typeof(BorderLessEditorRendrer))]
namespace HeySavings.Droid.CustomControls
{

    public class BorderLessEditorRendrer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}
