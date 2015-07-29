namespace Showcase.Server.Api.Infrastructure.Mapping
{
    public interface IMappingService
    {
        T Map<T>(object source);
    }
}
