namespace Showcase.Services.Data.Contracts
{
    using Showcase.Services.Common;

    public interface IUsersService : IService
    {
        int GetUserId(string username);
    }
}