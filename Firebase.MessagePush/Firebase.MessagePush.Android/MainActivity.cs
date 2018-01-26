using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.FirebasePushNotification;
using Android.Content;

namespace Firebase.MessagePush.Droid
{
    [Activity(Label = "Firebase.MessagePush", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            FirebasePushNotificationManager.ProcessIntent(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(intent);
        }
    }
}