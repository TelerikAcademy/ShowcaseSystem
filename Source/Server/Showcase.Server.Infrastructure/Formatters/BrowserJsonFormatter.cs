namespace Showcase.Server.Infrastructure.Formatters
{
    using System;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        private const string ApplicationJsonMediaType = "application/json";

        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ApplicationJsonMediaType));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue(ApplicationJsonMediaType);
            this.SerializerSettings.Formatting = Formatting.None;
            this.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
