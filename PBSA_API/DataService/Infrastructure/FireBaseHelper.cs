using DataService.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Infrastructure
{
    public class FireBaseHelper<T>
    {
        private FirebaseClient Firebase { get; }

        public FireBaseHelper(FirebaseConfig firebaseConfig)
        {
            var auth = firebaseConfig.Auth;
            var baseUrl = firebaseConfig.BaseURL;
            var option = new FirebaseOptions() { AuthTokenAsyncFactory = () => Task.FromResult(auth) };

            this.Firebase = new FirebaseClient(baseUrl, option);
        }


        public async Task<string> GetDataAsync(string key)
        {
            var data = await this.Firebase.Child(key).OnceAsync<string>();

            return data.ToString();
        }

        public async Task SetDataAsync(string key, T data)
        {
            var result = await this.Firebase.Child(key)
                .PostAsync(JsonConvert.SerializeObject(data));
        }

        public async Task PutDataAsync(string key, T data)
        {
            await this.Firebase.Child(key).PutAsync(JsonConvert.SerializeObject(data));
        }

        public async Task DeleteAsync(string key)
        {
            await this.Firebase.Child(key).DeleteAsync();
        }
    }
}
