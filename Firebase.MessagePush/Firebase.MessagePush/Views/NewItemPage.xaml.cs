using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Firebase.MessagePush.Models;
using System.Threading.Tasks;
using Plugin.Geolocator;
using System.Threading;

namespace Firebase.MessagePush.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Alert Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Alert();
            Task.Run(async () =>
            {
                try
                {
                    var locator = CrossGeolocator.Current;

                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5), null);

                    if (position == null)
                    {
                        return;
                    }
                    Item.Lat = position.Latitude;
                    Item.Lon = position.Longitude;
                }
                catch { }
            });
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}