namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Data.Models;

    public interface IDownloadableFilesService 
    {
        Task<IEnumerable<File>> AddNew(IEnumerable<RawFile> rawFiles);
    }
}
