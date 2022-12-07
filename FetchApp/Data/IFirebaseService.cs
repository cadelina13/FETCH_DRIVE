using FetchShared.Classes;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchApp.Data
{
    public interface IFirebaseService
    {
        Task AddItem<T>(T item);
        Task<T> GetItem<T>(string key);
        Task<List<T>> GetList<T>();
    }
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseClient client = new FirebaseClient("https://dirt-magnet-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public async Task AddItem<T>(T item)
        {
            await client.Child(typeof(T).Name).PostAsync(item.ToJSON(), true);
        }

        public async Task<T> GetItem<T>(string key)
        {
            return await client.Child($"{typeof(T).Name}/{key}").OnceSingleAsync<T>();
        }
        public async Task<List<T>> GetList<T>()
        {
            // to be implemented

            var collections = await client.Child(typeof(T).Name).OnceAsListAsync<T>();
            var items = collections.Select(x => x.Object).FirstOrDefault().ToJSON();
            return items.DeserializeJSON<List<T>>();
        }

        public void SubscribeItem<T>(ObservableCollection<T> list)
        {
            var collection = client
            .Child(typeof(T).Name)
            .AsObservable<T>()
            .Subscribe(item =>
            {
                if (item != null)
                {
                    list.Add(item.Object);
                }
            });
        }
    }
}
