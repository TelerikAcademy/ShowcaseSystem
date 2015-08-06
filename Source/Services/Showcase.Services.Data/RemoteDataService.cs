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
        // TODO: Pass as methods (or constructor) parameters
        private const string ApiKey = "5959c96cd1c787b26d72b28433178dcb"; // "3d33a038e0dbcaa7121c4f133dc474d7";

#if DEBUG
        private const string BaseAddress = "http://localhost:1337";
#else
        private const string BaseAddress = "https://telerikacademy.com";
#endif

        private const string ApiCheckUserLoginUrlFormat = "/Api/Users/CheckUserLogin?apiKey={0}&usernameoremail={1}&password={2}";
        private const string ApiGetUsersAvatarsUrlFormat = "/Api/Users/GetUsersAvatars?apiKey={0}&usernames={1}";
        private const string ApiSearchByUsernameUrlFormat = "/Api/Users/SearchByUsername?apiKey={0}&stringToSearch={1}&maxResults={2}";
        private const string ApiUserInfoUrlFormat = "/Api/Users/UserInfo?apiKey={0}&username={1}";
        private const string ApiAllGivenUsernamesExistsUrlFormat = "/Api/Users/AllGivenUsernamesExists?apiKey={0}&usernames={1}";

        private readonly HttpClient client;

        public RemoteDataService()
        {
            this.client = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            this.client.DefaultRequestHeaders.Add("Connection", "close");
        }

        // TODO: Pass API key
        public async Task<User> Login(string username, string password)
        {
            var url = string.Format(ApiCheckUserLoginUrlFormat, ApiKey, username, password);
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

        public async Task<IEnumerable<User>> UsersInfo(IEnumerable<string> usernames)
        {
            var url = string.Format(ApiGetUsersAvatarsUrlFormat, ApiKey, JsonConvert.SerializeObject(usernames));
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<IEnumerable<CheckUserLoginApiModel>>(jsonString);

            return
                model.Where(x => x.IsValid)
                    .Select(x => new User { UserName = x.UserName, AvatarUrl = x.SmallAvatarUrl, IsAdmin = x.IsAdmin });
        }

        public async Task<IEnumerable<string>> SearchByUsername(string username, int maxResults = 10)
        {
            var url = string.Format(ApiSearchByUsernameUrlFormat, ApiKey, username, maxResults);
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonString);
            return model;
        }

        public async Task<RemoteUserProfile> ProfileInfo(string username)
        {
            var url = string.Format(ApiUserInfoUrlFormat, ApiKey, username);
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<RemoteUserProfile>(jsonString);
            return model;
        }

        public async Task<bool> UsersExist(IEnumerable<string> usernames)
        {
            var url = string.Format(ApiAllGivenUsernamesExistsUrlFormat, ApiKey, JsonConvert.SerializeObject(usernames));
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<bool>(jsonString);
            return model;
        }
    }
}
