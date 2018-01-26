using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.MessagePush.Models;
using Foundation;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using UIKit;

namespace Firebase.MessagePush.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            FirebasePushNotificationManager.Initialize(options, true);
            return base.FinishedLaunching(app, options);
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
#if DEBUG
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken, FirebaseTokenType.Sandbox);
#endif
#if RELEASE
                    FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken,FirebaseTokenType.Production);
#endif
            

        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);

        }
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            System.Console.WriteLine(userInfo);

            try
            {
                var str = userInfo["body"].ToString();
                //var current = (App)Xamarin.Forms.Application.Current;
                //var alert = JsonConvert.DeserializeObject<Alert>(str);
                /// Calling if apply the activity of the alert reported
                //current.DisplayAlert(alert);
            }
            catch
            {

            }
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            FirebasePushNotificationManager.Connect();

        }
        public override void DidEnterBackground(UIApplication application)
        {
           FirebasePushNotificationManager.Disconnect();
        }
    }
}
