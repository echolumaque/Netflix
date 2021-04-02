using Android.Content;
using Netflix.Helpers.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomEntry), typeof(Netflix.Droid.Helpers.Renderers.CustomEntry))]
namespace Netflix.Droid.Helpers.Renderers
{
    public class CustomEntry : EntryRenderer
    {
        public CustomEntry(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
            }
        }
    }
}