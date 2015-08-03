namespace Showcase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Data.Remote.Models;

    public class RemoteDataService : IRemoteDataService
    {
#if DEBUG
        private const string BaseAddress = "http://localhost:1337";
#else
        private const string BaseAddress = "https://telerikacademy.com";
#endif
        private const string ApiCheckUserLoginUrlFormat = "/Api/Users/CheckUserLogin?usernameoremail={0}&password={1}";
        private const string ApiGetUsersAvatarsUrlFormat = "/Api/Users/GetUsersAvatars?usernames={0}";

        private readonly HttpClient client;

        public RemoteDataService()
        {
            this.client = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            this.client.DefaultRequestHeaders.Add("Connection", "close");
        }

        // TODO: Pass API key
        public async Task<User> Login(string username, string password)
        {
            var url = string.Format(ApiCheckUserLoginUrlFormat, username, password);
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CheckUserLoginApiModel>(jsonString);

            return model.IsValid
                       ? new User
                             {
                                 UserName = model.UserName,
                                 AvatarUrl = model.SmallAvatarUrl,
                                 IsAdmin = model.IsAdmin
                             }
                       : null;
        }

        // TODO: Pass API key
        public async Task<IEnumerable<User>> UsersInfo(IEnumerable<string> usernames)
        {
            var url = string.Format(ApiGetUsersAvatarsUrlFormat, JsonConvert.SerializeObject(usernames));
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<IEnumerable<CheckUserLoginApiModel>>(jsonString);

            return
                model.Where(x => x.IsValid)
                    .Select(x => new User { UserName = x.UserName, AvatarUrl = x.SmallAvatarUrl, IsAdmin = x.IsAdmin });
        }

        public Task<IEnumerable<string>> SearchByUsername(string username)
        {
            // TODO: get from telerikacademy.com all usernames which contain the search - return maximum 10 entries
            return Task.Run<IEnumerable<string>>(() => new List<string>
            {
                "ivaylo.kenov",
                "ivaylo.manekenov",
                "zdravko.georgiev",
                "zdravko.jelqzkov",
                "evlogi.hristov",
                "nikolay.kostov",
                "kolio",
                "kolio.TLa"
            }.Where(u => u.ToLower().Contains(username.ToLower()))); // for testing purpose, do not filter here
        }

        public Task<RemoteUserProfile> ProfileInfo(string username)
        {
            // TODO: get from telerikacademy.com and return null if user does not exist
            return Task.Run(() => new RemoteUserProfile
            {
                FirstName = "User",
                LastName = "Userov",
                Age = 10,
                City = "Petrich, Kalifornia",
                LargeAvatarUrl = "some.jpg",
                Occupation = "Director",
                Sex = "Male"
            });
        }

        public Task<bool> UsersExist(IEnumerable<string> usernames)
        {
            // TODO: return whether all usernames are valid users from telerikacademy.com
            return Task.Run(() => true);
        }
    }
}
