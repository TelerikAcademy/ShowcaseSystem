namespace Showcase.Server.DataTransferModels.User
{
    using System;

    public class UserAutocompleteResponseModel
    {
        public static Func<string, UserAutocompleteResponseModel> FromUserName
        {
            get
            {
                return username => new UserAutocompleteResponseModel
                {
                    Id = username,
                    Name = username
                };
            }
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
