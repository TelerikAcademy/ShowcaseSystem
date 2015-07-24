﻿namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ITagsService : IService
    {
        IQueryable<Tag> SearchByName(string name);
    }
}
