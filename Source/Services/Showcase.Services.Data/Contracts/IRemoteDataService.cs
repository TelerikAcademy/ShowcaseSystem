namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models.Remote;

    public interface IRemoteDataService : IService
    {
        Task<User> Login(string username, string password);

        Task<IEnumerable<User>> UsersInfo(IEnumerable<string> usernames);

        Task<IEnumerable<string>> SearchByUsername(string username, int maxResults = 10);

        Task<RemoteUserProfile> ProfileInfo(string username);

        Task<bool> UsersExist(IEnumerable<string> usernames);
    }
}
