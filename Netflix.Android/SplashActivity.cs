using System.Timers;
using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;

namespace Netflix.Droid
{
    [Activity(Theme = "@style/MainTheme.Base",
              MainLauncher = true,
              NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.Netflix);

            var timer = new Timer
            {
                Interval = 1500,
                AutoReset = false
            };
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
