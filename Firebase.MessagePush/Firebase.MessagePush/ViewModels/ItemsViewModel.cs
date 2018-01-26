using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.MessagePush.Models;
using Firebase.MessagePush.Views;
using Refit;
using Firebase.MessagePush.WepApiDefinition;
using Plugin.Geolocator;

namespace Firebase.MessagePush.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Alert> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private string URL_SERVICE = "http://localhost:55771";//"http://firebasepushnotificationtest.azurewebsites.net";
        public ItemsViewModel(Page control)
        {
            Title = "Browse";
            Items = new ObservableCollection<Alert>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Alert>(this, "AddItem", async (obj, item) =>
            {
                var locator = CrossGeolocator.Current;

                var position = await locator.GetLastKnownLocationAsync();
                if (position == null)
                {
                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5), null);
                }

                if (position == null)
                {
                    await control.DisplayAlert("Must Turn On Location Service", "Please turn on the Geolocation in order to calculate your current position.", "OK");
                    return;
                }
                var _item = item as Alert;
                if (_item != null && _item.Distance > 1 && _item.Distance < 11)
                {
                    _item.Lat = position.Latitude;
                    _item.Lon = position.Longitude;
                    Items.Add(_item);
                    await DataStore.AddItemAsync(_item);
                    var gitHubApi = RestService.For<IHttpAccessApi>(URL_SERVICE);
                    await gitHubApi.SendAlert(item);
                }
                else
                {
                    await control.DisplayAlert("Distance Value Incorrect", "The distance must be between 1 to 10.", "Close");
                    return;
                }
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}