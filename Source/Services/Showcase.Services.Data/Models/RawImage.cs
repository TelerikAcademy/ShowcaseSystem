namespace Showcase.Services.Data.Models
{
    public class RawImage
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public byte[] Content { get; set; }
    }
}
