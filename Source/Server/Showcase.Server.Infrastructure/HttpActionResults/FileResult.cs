namespace Showcase.Server.Infrastructure.HttpActionResults
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class FileResult : IHttpActionResult
    {
        private const string FileTypeHeaderValue = "application/octet-stream";
        private const string AttachmentContentDispositionHeaderValue = "attachment";

        private readonly FileStream fileStream;
        private readonly string fileName;

        public FileResult(FileStream fileStream, string fileName)
        {
            this.fileStream = fileStream;
            this.fileName = fileName;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(this.fileStream) };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(FileTypeHeaderValue);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(AttachmentContentDispositionHeaderValue)
            {
                FileName = this.fileName
            };

            return Task.FromResult(result);
        }
    }
}
