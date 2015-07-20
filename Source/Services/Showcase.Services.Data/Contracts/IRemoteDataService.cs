namespace Showcase.Services.Data.Contracts
{
    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IRemoteDataService : IService
    {
        User RemoteLogin(string username, string password);
    }
}
