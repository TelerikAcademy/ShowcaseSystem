namespace Showcase.Server.DataTransferModels.User
{
    using AutoMapper;
    using MissingFeatures;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;
    
    public class TeammateResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        //// public int ProjectId { get; set; }
          
        //// public string ProjectTitle { get; set; }
          
        //// public string TitleUrl
        //// {
        ////     get
        ////     {
        ////         return this.ProjectTitle.ToUrl();
        ////     }
        //// }

        public void CreateMappings(IConfiguration configuration)
        {
            //// configuration.CreateMap<User, TeammateResponseModel>()
            ////     .ForMember(u => u.ProjectId, opt => opt.ResolveUsing(x => (int)x.Context.Options.Items["projectId"]))
            ////     .ForMember(u => u.ProjectTitle, opt => opt.ResolveUsing(x => (string)x.Context.Options.Items["projectTitle"]));
        }
    }
}