using Firebase.MessagePush.Models;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Firebase.MessagePush.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
    {
		public MainPage ()
		{
			InitializeComponent ();

            masterPage.ListView.ItemSelected += OnItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Received");
                    if (p.Data.ContainsKey("body"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var text = $"{p.Data["body"]}";
                        });
                        try
                        {
                            var str = p.Data["body"].ToString();
                            var current = (App)Xamarin.Forms.Application.Current;
                            var alert = JsonConvert.DeserializeObject<Alert>(str);
                            /// Calling if apply the activity of the alert reported
                            current.DisplayAlert(alert);
                        }
                        catch
                        {

                        }
                    }
                }
                catch (Exception ex)
                {

                }

            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    try
                    {
                        var str = data.Value.ToString();
                        var current = (App)Xamarin.Forms.Application.Current;
                        var alert = JsonConvert.DeserializeObject<Alert>(str);
                        /// Calling if apply the activity of the alert reported
                        current.DisplayAlert(alert);
                    }
                    catch { }
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var _text = p.Identifier;
                    });
                }
                else if (p.Data.ContainsKey("aps.alert.title"))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var _text = $"{p.Data["aps.alert.title"]}";
                    });

                }
            };
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItemInfo;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}