using System.Threading.Tasks;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Netflix.Helpers.Dependency;
using Plugin.CurrentActivity;
using Xamarin.Essentials;
using Xamarin.Forms;
using Color = Android.Graphics.Color;

namespace Netflix.Droid.Helpers.Dependency
{
    public class ChangeWindow : IChangeWindow
    {
        private static Window current = CurrentWindow();
        void IChangeWindow.ChangeWindow(bool onHomePage)
        {
            //    if (onHomePage)
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            if (Build.VERSION.SdkInt <= BuildVersionCodes.Q)
            //            {
            //                current.SetStatusBarColor(Color.Transparent);
            //                current.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            //                current.AddFlags(WindowManagerFlags.LayoutInOverscan);
            //                current.AddFlags(WindowManagerFlags.ForceNotFullscreen);
            //            }
            //            else if (Build.VERSION.SdkInt == BuildVersionCodes.R || Build.VERSION.SdkInt != BuildVersionCodes.R)
            //            {
            //                current.SetStatusBarColor(Color.Transparent);
            //                current.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            //                current.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            //                current.AddFlags(WindowManagerFlags.LayoutInOverscan);
            //                current.AddFlags(WindowManagerFlags.ForceNotFullscreen);
            //                current.SetDecorFitsSystemWindows(false);
            //            }
            //            else
            //            {
            //                current.SetStatusBarColor(Color.Transparent);
            //                current.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Never;
            //                current.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            //                current.AddFlags(WindowManagerFlags.LayoutInOverscan);
            //                current.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            //            }
            //        });
            //    }
            //    else
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            if (Build.VERSION.SdkInt <= BuildVersionCodes.Q)
            //            {
            //                current.SetStatusBarColor(Color.Transparent);
            //                current.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            //                current.AddFlags(WindowManagerFlags.LayoutInOverscan);
            //                current.AddFlags(WindowManagerFlags.Fullscreen);
            //            }
            //            else if (Build.VERSION.SdkInt == BuildVersionCodes.R || Build.VERSION.SdkInt != BuildVersionCodes.R)
            //            {
            //                current.SetStatusBarColor(Color.Transparent);
            //                current.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            //                current.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            //                current.AddFlags(WindowManagerFlags.LayoutInOverscan);
            //                current.AddFlags(WindowManagerFlags.Fullscreen);
            //                current.SetDecorFitsSystemWindows(false);
            //            }
            //            else
            //            {
            //                current.SetStatusBarColor(Color.Transparent);
            //                current.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Never;
            //                current.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            //                current.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
            //                current.AddFlags(WindowManagerFlags.LayoutInScreen);
            //                current.ClearFlags(WindowManagerFlags.Fullscreen);
            //            }
            //        });
            //    }
            }

            private static Window CurrentWindow() => CrossCurrentActivity.Current.Activity.Window;
    }
}