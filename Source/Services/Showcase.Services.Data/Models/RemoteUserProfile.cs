namespace Showcase.Services.Data.Models
{
    public class RemoteUserProfile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string Occupation { get; set; }

        public string LargeAvatarUrl { get; set; } // the biggest resolution of the avatar
    }
}
