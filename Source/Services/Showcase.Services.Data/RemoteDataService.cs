namespace Showcase.Services.Data
{
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class RemoteDataService : IRemoteDataService
    {
        public User RemoteLogin(string username, string password)
        {
            // TODO: implement and get from telerikacademy.com, data and two avatars
            return new User
            {
                UserName = username,
                AvatarUrl = "some url",
                IsAdmin = true
            };
        }
    }
}
