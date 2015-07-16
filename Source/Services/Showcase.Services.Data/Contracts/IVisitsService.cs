namespace Showcase.Services.Data.Contracts
{
    using Showcase.Services.Common;

    public interface IVisitsService : IService
    {
        void VisitProject(int projectId, string username);
    }
}