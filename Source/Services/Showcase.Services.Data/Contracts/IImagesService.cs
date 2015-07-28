namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IImagesService : IService
    {
        IEnumerable<ProcessedImage> ProcessImages(IEnumerable<RawImage> rawImages);
    }
}
