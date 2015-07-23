namespace Showcase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data;
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IRemoteDataService remoteData;

        public UsersService(IRepository<User> users, IRemoteDataService remoteData)
        {
            this.users = users;
            this.remoteData = remoteData;
        }

        public int GetUserId(string username)
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
                .Where(u => u.UserName == username);
        }

        public IEnumerable<string> SearchByUsername(string username)
        {
            return this.remoteData.SearchByUsername(username);
        }

        public async Task<User> GetAccountAsync(string username, string password)
        {
            var remoteUser = this.remoteData.RemoteLogin(username, password);
            if (remoteUser == null)
            {
                return null;
            }

            var localUser = await this.GetLocalAccountAsync(username); // TODO: update user every time
            if (localUser == null)
            {
                localUser = new User
                {
                    UserName = remoteUser.UserName,
                    AvatarUrl = remoteUser.AvatarUrl,
                    IsAdmin = remoteUser.IsAdmin
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

        private async Task<User> GetLocalAccountAsync(string username)
        {
            return await this.users
                .All()
                .FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}