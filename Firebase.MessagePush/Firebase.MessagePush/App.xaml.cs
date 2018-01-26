using System;
using System.Threading.Tasks;
using Firebase.MessagePush.Helpers;
using Firebase.MessagePush.Models;
using Firebase.MessagePush.Services;
using Firebase.MessagePush.ViewModels;
using Firebase.MessagePush.Views;
using Plugin.FirebasePushNotification;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace Firebase.MessagePush
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();


            MainPage = new MainPage();
        }
        public void DisplayAlert(Alert item)
        {
            Task.Run(async () =>
            {
                var locator = CrossGeolocator.Current;

                var position = await locator.GetLastKnownLocationAsync();
                double distance = 1;
                if (position != null)
                {
                    distance = new Coordinates(position.Latitude, position.Longitude)
                       .DistanceTo(
                           new Coordinates(item.Lat, item.Lon),
                           UnitOfLength.Kilometers
                       );
                }

                if (distance <= item.Distance)
                {
                    var dataStore = new MockDataStore();
                    await dataStore.AddItemAsync(item);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var view = new ItemDetailPage(new ItemDetailViewModel(item));
                        await MainPage.Navigation.PushModalAsync(view);
                    });
                }
            });
        }
        protected override void OnStart ()
        {
            CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            };
            System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
