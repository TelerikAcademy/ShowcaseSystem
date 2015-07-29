namespace Showcase.Services.Data.Contracts
{
    using Showcase.Services.Common;

    public interface IFlagsService : IService
    {
        void FlagProject(int projectId, string username);

        void UnFlagProject(int projectId, string username);

        bool ProjectIsFlaggedByUser(int projectId, string username);
    }
}