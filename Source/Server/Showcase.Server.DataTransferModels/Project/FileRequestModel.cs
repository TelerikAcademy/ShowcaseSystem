namespace Showcase.Server.DataTransferModels.Project
{
    using System;

    public class FileRequestModel
    {
        public string OriginalName { get; set; }

        public string FileExtension { get; set; }

        public string Base64Content { get; set; }

        public byte[] ByteArrayContent
        {
            get
            {
                return Convert.FromBase64String(this.Base64Content);
            }
        }
    }
}
