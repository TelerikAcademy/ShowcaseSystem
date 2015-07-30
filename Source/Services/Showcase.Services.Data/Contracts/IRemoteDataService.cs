﻿namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IRemoteDataService : IService
    {
        User Login(string username, string password);

        Task<IEnumerable<User>> UsersInfo(IEnumerable<string> usernames);

        IEnumerable<string> SearchByUsername(string username);

        RemoteUserProfile ProfileInfo(string username);

        bool UsersExist(IEnumerable<string> usernames);
    }
}
