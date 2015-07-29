namespace Showcase.Services.Logic
{
    using AutoMapper;

    using Showcase.Services.Logic.Contracts;

    public class MappingService : IMappingService
    {
        public T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
