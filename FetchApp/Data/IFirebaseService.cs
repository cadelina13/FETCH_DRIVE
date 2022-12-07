using FetchShared.Classes;
using FetchShared.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using MudBlazor.Extensions;
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
        Task<string> AddItemAsync<T>(T item);
        Task UpdateItemAsync<T>(string key, T item);
        Task DeleteItemAsync<T>(string key);
        Task<T> GetItemAsync<T>(string key);
        Task<List<Wrapper<T>>> GetListAsync<T>();
        Task<List<Wrapper<T>>> GetListAsync<T>(int limit);
        Task<List<Wrapper<T>>> FindAsync<T>(string str);
        IObservable<FirebaseEvent<T>> Subscribe<T>(ObservableCollection<T> data);
        void Dispose();
    }

    public class FirebaseService : IFirebaseService, IDisposable
    {
        private readonly FirebaseClient client = new FirebaseClient("https://fetchdrive-2fe73-default-rtdb.asia-southeast1.firebasedatabase.app/");
        private readonly string UserId = "09163617169";
        public async Task<string> AddItemAsync<T>(T item)
        {
            var rs = await client.Child($"{typeof(T).Name}/{UserId}").PostAsync(item.ToJSON(), true);
            return rs.Key;
        }
        public async Task UpdateItemAsync<T>(string key, T item)
        {
            await client.Child($"{typeof(T).Name}/{UserId}/{key}").PutAsync(item.ToJSON());
        }
        public async Task DeleteItemAsync<T>(string key)
        {
            await client.Child($"{typeof(T).Name}/{UserId}/{key}").DeleteAsync();
        }
        public async Task<T> GetItemAsync<T>(string key)
        {
            return await client.Child($"{typeof(T).Name}/{UserId}/{key}").OnceSingleAsync<T>();
        }
        public async Task<List<Wrapper<T>>> GetListAsync<T>()
        {
            string path = $"{typeof(T).Name}/{UserId}";
            var collections = await client.Child(path).OrderByKey().OnceAsync<T>();
            var result = new List<Wrapper<T>>();
            foreach (var item in collections)
            {
                var wr = new Wrapper<T>();
                wr.Object = item.Object;
                wr.Key = item.Key;
                result.Add(wr);
            }
            return result;
        }
        public async Task<List<Wrapper<T>>> GetListAsync<T>(int limit)
        {
            string path = $"{typeof(T).Name}/{UserId}";
            var collections = await client.Child(path).OrderByKey().LimitToFirst(limit).OnceAsync<T>();
            var result = new List<Wrapper<T>>();
            foreach (var item in collections)
            {
                var wr = new Wrapper<T>();
                wr.Object = item.Object;
                wr.Key = item.Key;
                result.Add(wr);
            }
            return result;
        }
        public async Task<List<Wrapper<T>>> FindAsync<T>(string str)
        {
            string path = $"{typeof(T).Name}/{UserId}";
            var collections = await client.Child(path).OrderByKey().StartAt(str).LimitToFirst(10).OnceAsync<T>();
            var result = new List<Wrapper<T>>();
            foreach (var item in collections)
            {
                var wr = new Wrapper<T>();
                wr.Object = item.Object;
                wr.Key = item.Key;
                result.Add(wr);
            }
            return result;
        }
        public IObservable<FirebaseEvent<T>> Subscribe<T>(ObservableCollection<T> data)
        {
            string path = $"{typeof(T).Name}/{UserId}";

            var rs = new ObservableCollection<T>();
            var collection = client
            .Child(path)
            .AsObservable<T>().Subscribe(item =>
            {
                data.Add(item.Object);
            });
            return default;
        }

        public void Dispose() => client.Dispose();
    }
}
