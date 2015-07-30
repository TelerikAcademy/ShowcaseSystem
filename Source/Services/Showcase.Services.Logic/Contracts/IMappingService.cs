namespace Showcase.Services.Logic.Contracts
{
    using Showcase.Services.Common;

    public interface IMappingService : IService
    {
        T Map<T>(object source);
    }
}
