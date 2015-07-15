namespace Showcase.Services.Data.Contracts
{
    using Showcase.Services.Common;

    public interface IStatisticsService : IService
    {
        object Current();
    }
}
