namespace Showcase.Services.Data
{
    using System;
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public int GetUserId(string username)
        {
            return this.users
                .All()
                .Where(u => u.Username == username)
                .Select(u => u.Id)
                .FirstOrDefault();
        }
    }
}