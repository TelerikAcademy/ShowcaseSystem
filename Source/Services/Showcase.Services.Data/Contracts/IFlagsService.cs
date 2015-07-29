namespace Showcase.Services.Data.Contracts
{
    public interface IFlagsService
    {
        void FlagProject(int projectId, string username);

        void UnFlagProject(int projectId, string username);
    }
}