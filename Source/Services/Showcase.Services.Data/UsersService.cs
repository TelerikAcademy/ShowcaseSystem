namespace Showcase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models.Remote;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IRemoteDataService remoteData;

        public UsersService(IRepository<User> users, IRemoteDataService remoteData)
        {
            this.users = users;
            this.remoteData = remoteData;
        }

        public async Task<int> UserIdByUsername(string username)
        {
            return await this.users
                .All()
                .Where(u => u.UserName == username)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UserIsAdmin(string username)
        {
            return await this.users
                .All()
                .Where(u => u.UserName == username)
                .Select(u => u.IsAdmin)
                .FirstOrDefaultAsync();
        }

        public IQueryable<User> ByUsername(string username)
        {
            return this.users
                .All()
                .Where(u => u.UserName == username);
        }

        public async Task<IEnumerable<string>> SearchByUsername(string username)
        {
            return await this.remoteData.SearchByUsername(username);
        }

        public async Task<User> Account(string username, string password)
        {
            var remoteUser = await this.remoteData.Login(username, password);
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

        public async Task<ICollection<User>> CollaboratorsFromCommaSeparatedValues(string collaborators, string currentUserUsername = null)
        {
            var usernames = new HashSet<string>();
            if (!string.IsNullOrWhiteSpace(collaborators))
            {
                usernames = collaborators.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            if (!string.IsNullOrWhiteSpace(currentUserUsername))
            {
                usernames.Add(currentUserUsername);
            }

            var localUsers = await this.users
                .All()
                .Where(u => usernames.Contains(u.UserName))
                .ToListAsync();

            var nonExistingLocalUsernames = usernames.Where(username => localUsers.All(u => u.UserName != username));
            var nonExistingLocalUsersRemoteInfo = await this.remoteData.UsersInfo(nonExistingLocalUsernames);

            localUsers.AddRange(nonExistingLocalUsersRemoteInfo);
            return localUsers;
        }

        public async Task<bool> UserIsCollaboratorInProject(int projectId, string userName)
        {
            return await this.users
                .All()
                .Where(u => u.UserName == userName && u.Projects.Any(pr => pr.Id == projectId))
                .AnyAsync();
        }

        public async Task<RemoteUserProfile> ProfileInfo(string username)
        {
            return await this.remoteData.ProfileInfo(username);
        }

        private async Task<User> GetLocalAccount(string username)
        {
            return await this.users
                .All()
                .FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}