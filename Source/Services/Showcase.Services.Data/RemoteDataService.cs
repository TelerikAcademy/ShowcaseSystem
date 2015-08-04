﻿namespace Showcase.Services.Data
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
        private const string ApiSearchByUsernameUrlFormat = "/Api/Users/SearchByUsername?stringToSearch={0}&maxResults={1}";
        private const string ApiUserInfoUrlFormat = "/Api/Users/UserInfo?username={0}";
        private const string ApiAllGivenUsernamesExistsUrlFormat = "/Api/Users/AllGivenUsernamesExists?usernames={0}";

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

        // TODO: Pass API key
        public async Task<IEnumerable<string>> SearchByUsername(string username, int maxResults = 10)
        {
            var url = string.Format(ApiSearchByUsernameUrlFormat, username, maxResults);
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonString);
            return model;
        }

        // TODO: Pass API key
        public async Task<RemoteUserProfile> ProfileInfo(string username)
        {
            var url = string.Format(ApiUserInfoUrlFormat, username);
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<RemoteUserProfile>(jsonString);
            return model;
        }

        // TODO: Pass API key
        public async Task<bool> UsersExist(IEnumerable<string> usernames)
        {
            var url = string.Format(ApiAllGivenUsernamesExistsUrlFormat, JsonConvert.SerializeObject(usernames));
            var response = await this.client.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<bool>(jsonString);
            return model;
        }
    }
}
