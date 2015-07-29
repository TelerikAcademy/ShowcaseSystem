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
    using Showcase.Services.Common.Extensions;
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
            var remoteUser = this.remoteData.Login(username, password);
            if (remoteUser == null)
            {
                return null;
            }

            var localUser = await this.GetLocalAccountAsync(username);
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
            else if (localUser.AvatarUrl != remoteUser.AvatarUrl || localUser.IsAdmin != remoteUser.IsAdmin)
            {
                localUser.IsAdmin = remoteUser.IsAdmin;
                localUser.AvatarUrl = remoteUser.AvatarUrl;
                this.users.SaveChanges();
            }

            return localUser;
        }

        public ICollection<User> GetCollaboratorsFromCommaSeparatedValues(string collaborators)
        {
            var usernames = collaborators.Split(',');
            var localUsers = this.users
                .All()
                .Where(u => usernames.Contains(u.UserName))
                .ToList();

            var nonExistingLocalUsernames = usernames.Where(username => localUsers.All(u => u.UserName != username));
            var nonExistingLocalUsersRemoteInfo = this.remoteData.UsersInfo(nonExistingLocalUsernames);

            // TODO: uncomment when RemoteDataService is implemented
            // var newlyAddedUsers = this.AddNonExistingUsers(nonExistingLocalUsersRemoteInfo);
            // localUsers.AddRange(newlyAddedUsers);
            return localUsers;
        }

        private async Task<User> GetLocalAccountAsync(string username)
        {
            return await this.users
                .All()
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        private IEnumerable<User> AddNonExistingUsers(IEnumerable<User> usersToAdd)
        {
            usersToAdd.ForEach(user => this.users.Add(user));
            this.users.SaveChanges();
            return usersToAdd;
        }
    }
}