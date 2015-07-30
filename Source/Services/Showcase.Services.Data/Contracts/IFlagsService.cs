namespace Showcase.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Showcase.Services.Common;

    public interface IFlagsService : IService
    {
        Task FlagProject(int projectId, string username);

        Task UnFlagProject(int projectId, string username);

        Task<bool> ProjectIsFlaggedByUser(int projectId, string username);
    }
}