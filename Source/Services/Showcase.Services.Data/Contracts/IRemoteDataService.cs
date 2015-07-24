namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IRemoteDataService : IService
    {
        User RemoteLogin(string username, string password);

        IEnumerable<string> SearchByUsername(string username);

        RemoteUserProfile ProfileInfo(string username);

        bool UsersExist(IEnumerable<string> usernames);
    }
}
