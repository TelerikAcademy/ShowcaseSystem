namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public class DownloadableFilesService : IDownloadableFilesService
    {
        private readonly IObjectFactory objectFactory;

        public DownloadableFilesService(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public async Task<IEnumerable<File>> AddNew(IEnumerable<RawFile> rawFiles)
        {
            // TODO: implement base class with commong method for image and file
            var files = await rawFiles.ForEachAsync(async rawFile =>
            {
                var file = new File { OriginalFileName = rawFile.OriginalFileName, FileExtension = rawFile.FileExtension };
                var filesContext = this.objectFactory.GetInstance<ShowcaseDbContext>();
                filesContext.Files.Add(file);
                await filesContext.SaveChangesAsync();

                file.UrlPath = file.Id.ToUrlPath();
                await filesContext.SaveChangesAsync();

                return file;
            });

            return files;
        }
    }
}
