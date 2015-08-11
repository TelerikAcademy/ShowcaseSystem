namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Base;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public class DownloadableFilesService : FileInfoService, IDownloadableFilesService
    {
        public DownloadableFilesService(IObjectFactory objectFactory)
            : base(objectFactory)
        {
        }

        public async Task<IEnumerable<File>> AddNew(IEnumerable<RawFile> rawFiles)
        {
            var files = await rawFiles.ForEachAsync(async rawFile =>
            {
                var file = await base.SaveFileInfo<File>(rawFile);
                file.Content = rawFile.Content;
                return file;
            });

            return files;
        }
    }
}
