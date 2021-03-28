using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Netflix.Helpers.Dependency;

namespace Netflix.Droid.Helpers.Dependency
{
    public class ToastDroid : IToast
    {
        public void ShowToast(string message) => Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
    }
}