namespace Showcase.Services.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Showcase.Data;
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IRemoteDataService remoteData;

        public UsersService(IRepository<User> users, IRemoteDataService remoteData)
        {
            this.users = users;
            this.remoteData = remoteData;
        }

        public string GetUserId(string username)
        {
            return this.users
                .All()
                .Where(u => u.UserName == username)
                .Select(u => u.Id)
                .FirstOrDefault();
        }

        public IQueryable<User> GetByUsername(string username)
        {
            return this.users
                .All()
                .Where(u => u.Username == username);
        }

        public async Task<User> GetAccountAsync(string username, string password)
        {
            var remoteUser = remoteData.RemoteLogin(username, password);
            if (remoteUser == null)
            {
                return null;
            }

            var localUser = await this.GetLocalAccount(username);
            if (localUser == null)
            {
                localUser = new User
                {
                    UserName = remoteUser.UserName,
                    AvatarUrl = remoteUser.AvatarUrl
                };

                this.users.Add(localUser);
                this.users.SaveChanges();
            }
            else if (localUser.AvatarUrl != remoteUser.AvatarUrl)
            {
                localUser.AvatarUrl = remoteUser.AvatarUrl;
                this.users.SaveChanges();
            }

            return localUser;
        }

        private async Task<User> GetLocalAccount(string username)
        {
            return await this.users
                .All()
                .FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}