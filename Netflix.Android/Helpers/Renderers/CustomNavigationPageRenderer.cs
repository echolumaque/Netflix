using AView = Android.Views.View;
using Android.Content;
using Netflix.Droid.Helpers.Renderers;
using Netflix.Helpers.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomNavigation), typeof(CustomNavigationPageRenderer))]
namespace Netflix.Droid.Helpers.Renderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        public CustomNavigationPageRenderer(Context context) : base(context)
        {

        }

        IPageController PageController => Element as IPageController;
        CustomNavigation CustomNavigationPage => Element as CustomNavigation;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            CustomNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            CustomNavigationPage.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is AndroidX.AppCompat.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}