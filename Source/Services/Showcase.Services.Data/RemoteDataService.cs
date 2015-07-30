namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;

    public class RemoteDataService : IRemoteDataService
    {
        public Task<User> Login(string username, string password)
        {
            // TODO: implement and get from telerikacademy.com
            return Task.Run(() => new User
            {
                UserName = username,
                AvatarUrl = "some url", // return small avatar URL here 
                IsAdmin = true
            });
        }

        public Task<IEnumerable<User>> UsersInfo(IEnumerable<string> usernames)
        {
            // TODO: return user information from telerikacademy.com
            return Task.Run<IEnumerable<User>>(() => new List<User>
            {
                new User
                {
                    UserName = "some user",
                    AvatarUrl = "some url", // return small avatar URL here 
                    IsAdmin = false
                },
                new User
                {
                    UserName = "another user",
                    AvatarUrl = "another url", // return small avatar URL here 
                    IsAdmin = false
                }
            });
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
