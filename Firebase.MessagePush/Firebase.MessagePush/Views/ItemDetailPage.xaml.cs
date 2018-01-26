using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Firebase.MessagePush.Models;
using Firebase.MessagePush.ViewModels;

namespace Firebase.MessagePush.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Alert
            {
                Message = "Write your alert"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}