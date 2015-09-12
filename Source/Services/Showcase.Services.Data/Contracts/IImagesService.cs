﻿namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IImagesService : IService
    {
        Task<IEnumerable<ProcessedImage>> ProcessImages(IEnumerable<RawFile> rawImages);

        Task<bool> ValidateImageUrls(ICollection<string> imageUrls);
    }
}
