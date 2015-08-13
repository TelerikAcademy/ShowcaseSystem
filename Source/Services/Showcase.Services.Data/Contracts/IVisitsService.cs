namespace Showcase.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Showcase.Services.Common;

    public interface IVisitsService : IService
    {
        Task VisitProject(int projectId);
    }
}