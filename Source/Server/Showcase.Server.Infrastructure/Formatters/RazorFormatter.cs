namespace Showcase.Server.Infrastructure.Formatters
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Hosting;

    using RazorEngine;
    using RazorEngine.Templating;

    using Showcase.Server.DataTransferModels.Project;

    using TextEncoding = System.Text.Encoding;

    public class RazorFormatter : MediaTypeFormatter
    {
        private const string TextHtmlMediaType = "text/html";
        private const string ApplicationXHtmlMediaType = "application/xhtml+xml";

        private const string CrawlersIndexView = "~/crawlers.cshtml";

        public RazorFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(TextHtmlMediaType));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ApplicationXHtmlMediaType));
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(ProjectCrawlerResponseModel))
            {
                return true;
            }

            return false;
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var task = Task.Factory.StartNew(() =>
            {
                var viewPath = HostingEnvironment.MapPath(CrawlersIndexView);
                //// TODO: viewPath can be null
                var template = File.ReadAllText(viewPath);

                if (!Engine.Razor.IsTemplateCached(type.Name, type))
                {
                    Engine.Razor.Compile(template, type.Name, type);
                }

                var razor = Engine.Razor.Run(type.Name, type, value);

                var buf = TextEncoding.Default.GetBytes(razor);
                writeStream.Write(buf, 0, buf.Length);
                writeStream.Flush();
            });

            return task;
        }
    }
}
