namespace Showcase.Services.Data.Models.Remote
{
    public class RemoteUserProfile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets the biggest resolution of the avatar
        /// </summary>
        public string ProfileAvatarUrl { get; set; }
    }
}
