using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.MessagePush.Interfaces;
using Firebase.MessagePush.Models;
using LiteDB;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Firebase.MessagePush.Services.MockDataStore))]
namespace Firebase.MessagePush.Services
{
    public class MockDataStore : IDataStore<Alert>
    {
        List<Alert> items;
        string constr = "";

        public MockDataStore()
        {
            items = new List<Alert>();
            constr = DependencyService.Get<IFileHelper>().GetLocalFilePath("MyData.db");
        }

        public async Task<bool> AddItemAsync(Alert item)
        {
            items.Add(item);
            using (var db = new LiteDatabase(constr))
            {
                var col = db.GetCollection<Alert>("Alert");
                try
                {
                    var document = col.FindById(item.Id);
                    if (document != null)
                    {
                        col.Update(item);
                    }
                    else
                    {
                        col.Insert(item);
                    }
                }
                catch { }
                var list = col.FindAll();
                if (list != null)
                {
                    items = list.ToList();
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Alert item)
        {
            using (var db = new LiteDatabase(constr))
            {
                var col = db.GetCollection<Alert>("Alert");

                col.Update(item);
                var list = col.FindAll();
                if (list != null)
                {
                    items = list.ToList();
                }
            }
            
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Alert item)
        {
            var _item = items.Where((Alert arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            
            using (var db = new LiteDatabase(constr))
            {
                var col = db.GetCollection<Alert>("Alert");
                
                col.Delete(g => g.Id == item.Id);
                
                var list = col.FindAll();
                if (list != null)
                {
                    items = list.ToList();
                }
            }

            return await Task.FromResult(true);
        }

        public async Task<Alert> GetItemAsync(string id)
        {
            using (var db = new LiteDatabase(constr))
            {
                var col = db.GetCollection<Alert>("Alert");
                var list = col.FindAll();
                if (list != null)
                {
                    items = list.ToList();
                }
            }
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Alert>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var db = new LiteDatabase(constr))
            {
                var col = db.GetCollection<Alert>("Alert");
                var list = col.FindAll();
                if (list != null)
                {
                    items = list.ToList();
                }
            }
            return await Task.FromResult(items);
        }
    }
}