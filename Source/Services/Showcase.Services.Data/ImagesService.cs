namespace Showcase.Services.Data
{
    using System.Collections.Generic;

    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;

    public class ImagesService : IImagesService
    {
        public IEnumerable<Image> ProcessImages(IEnumerable<RawImage> rawImages)
        {
            throw new System.NotImplementedException();
        }
    }
}
