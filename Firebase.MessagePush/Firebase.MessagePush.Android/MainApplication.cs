using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Firebase.MessagePush.Models;
using Newtonsoft.Json;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;

namespace Firebase.MessagePush.Droid
{
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }
        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);

            //If debug you should reset the token each time.
#if DEBUG
            FirebasePushNotificationManager.Initialize(this, true);
#else
            FirebasePushNotificationManager.Initialize(this, false);
#endif
            //Handle notifications when app closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("NOTIFICATION RECEIVED", p.Data);
                try
                {
                    var str = p.Data["body"].ToString();
                    //var current = (App)Xamarin.Forms.Application.Current;
                    //var alert = JsonConvert.DeserializeObject<Alert>(str);
                    /// Calling if apply the activity of the alert reported
                    //current.DisplayAlert(alert);
                }
                catch
                {
                    
                }
               
                   // current.DisplayAlert(intent.GetStringExtra("Data"));

            };

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }
        }

    }
}