namespace Showcase.Server.Common.Mapping
{
    using AutoMapper;

    using Showcase.Data.Models;
    using Showcase.Services.Data.Models;

    public class ExplicitMaps
    {
        public static void AddMaps(IConfiguration configuration)
        {
            configuration.CreateMap<Image, ProcessedImage>().ReverseMap();
        }
    }
}
