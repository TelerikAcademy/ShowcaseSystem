namespace Showcase.Services.Data.Remote.Models
{
    public class CheckUserLoginApiModel
    {
        public bool IsValid { get; set; }

        public string UserName { get; set; }

        public string SmallAvatarUrl { get; set; }

        public bool IsAdmin { get; set; }
    }
}
