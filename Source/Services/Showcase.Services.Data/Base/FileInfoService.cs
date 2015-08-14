namespace Showcase.Services.Data.Base
{
    using System;
    using System.Threading.Tasks;

    using Showcase.Data;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public abstract class FileInfoService
    {
        private const char WhiteSpace = ' ';

        private readonly IObjectFactory objectFactory;

        protected FileInfoService(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public async Task<T> SaveFileInfo<T>(RawFile file)
            where T : FileInfo, new()
        {
            var processedFileName = string.Join(WhiteSpace.ToString(), file.OriginalFileName.Split(new[] { WhiteSpace }, StringSplitOptions.RemoveEmptyEntries));
            var databaseFile = new T { OriginalFileName = processedFileName, FileExtension = file.FileExtension };

            var filesContext = this.objectFactory.GetInstance<ShowcaseDbContext>();
            filesContext.Set<T>().Add(databaseFile);
            await filesContext.SaveChangesAsync();

            databaseFile.UrlPath = databaseFile.Id.ToUrlPath();
            await filesContext.SaveChangesAsync();

            return databaseFile;
        }
    }
}
