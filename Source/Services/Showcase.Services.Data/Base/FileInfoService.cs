namespace Showcase.Services.Data.Base
{
    using System.Threading.Tasks;

    using Showcase.Data;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public abstract class FileInfoService
    {
        private readonly IObjectFactory objectFactory;

        public FileInfoService(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public async Task<T> SaveFileInfo<T>(RawFile file)
            where T : FileInfo, new()
        {
            var databaseFile = new T { OriginalFileName = file.OriginalFileName, FileExtension = file.FileExtension };

            var filesContext = this.objectFactory.GetInstance<ShowcaseDbContext>();
            filesContext.Set<T>().Add(databaseFile);
            await filesContext.SaveChangesAsync();

            databaseFile.UrlPath = databaseFile.Id.ToUrlPath();
            await filesContext.SaveChangesAsync();

            return databaseFile;
        }
    }
}
