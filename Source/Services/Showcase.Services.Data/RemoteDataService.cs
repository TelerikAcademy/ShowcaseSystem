namespace Showcase.Services.Data
{
    using System.Collections.Generic;

    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;
using Showcase.Services.Data.Models;

    public class RemoteDataService : IRemoteDataService
    {
        public User RemoteLogin(string username, string password)
        {
            // TODO: implement and get from telerikacademy.com, data and two avatars
            return new User
            {
                UserName = username,
                AvatarUrl = "some url", // return small avatar URL here 
                IsAdmin = true
            };
        }

        public IEnumerable<string> SearchByUsername(string username)
        {
            // TODO: get from telerikacademy.com all usernames which contain the search
            return new List<string>
            {
                "ivaylo.kenov",
                "ivaylo.manekenov",
                "zdravko.georgiev",
                "zdravko.jelqzkov",
                "evlogi.hristov",
                "nikolay.kostov",
                "kolio",
                "kolio.TLa"
            };
        }

        public RemoteUserProfile ProfileInfo(string username)
        {
            // TODO: get from telerikacademy.com
            return new RemoteUserProfile
            {
                FirstName = "User",
                LastName = "Userov",
                Age = 10,
                City = "Petrich, Kalifornia",
                LargeAvatarUrl = "some.jpg",
                Occupation = "Director",
                Sex = "Male"
            };
        }
    }
}
