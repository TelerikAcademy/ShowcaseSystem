namespace Showcase.Server.Api.Infrastructure
{
    using System;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
            this.SerializerSettings.Formatting = Formatting.None;
            this.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
