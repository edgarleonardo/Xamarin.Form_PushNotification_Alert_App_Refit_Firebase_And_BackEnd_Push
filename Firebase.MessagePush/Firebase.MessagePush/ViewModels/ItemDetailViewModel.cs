using System;

using Firebase.MessagePush.Models;

namespace Firebase.MessagePush.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Alert Item { get; set; }
        public ItemDetailViewModel(Alert item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
