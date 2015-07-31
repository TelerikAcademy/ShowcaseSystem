namespace Showcase.Server.DataTransferModels.Project
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using MissingFeatures;
    
    using Showcase.Data.Models;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Mapping;
    using Showcase.Server.DataTransferModels.Tag;

    public class ProjectResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public const int ShortDescriptionLength = 300;

        public int Id { get; set; }

        public string Name { get; set; }

        public string MainImageUrl { get; set; }

        public string Description { get; set; }

        public string RepositoryUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Visits { get; set; }

        public int Comments { get; set; }

        public IEnumerable<string> Collaborators { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }

        public IEnumerable<TagResponseModel> Tags { get; set; }

        public bool IsLiked { get; set; }

        public bool IsFlagged { get; set; }

        public string TitleUrl
        {
            get
            {
                return this.Name.ToUrl();
            }
        }

        public string ShortDate
        {
            get
            {
                return this.CreatedOn.ToString(Constants.ShortDateFormat);
            }
        }

        public string ShortDescription 
        {
            get
            {
                if (this.Description.Length <= ProjectResponseModel.ShortDescriptionLength)
                {
                    return this.Description;
                }
                else
                {
                    return this.Description.Substring(0, ProjectResponseModel.ShortDescriptionLength) + "...";
                }
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            string username = null;
            configuration.CreateMap<Project, ProjectResponseModel>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(pr => pr.Title))
                .ForMember(pr => pr.Likes, opt => opt.MapFrom(pr => pr.Likes.Count))
                .ForMember(pr => pr.Visits, opt => opt.MapFrom(pr => pr.Visits.Count))
                .ForMember(pr => pr.Comments, opt => opt.MapFrom(pr => pr.Comments.Count))
                .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.UrlPath))
                .ForMember(pr => pr.ImageUrls, opt => opt.MapFrom(pr => pr.Images.Select(i => i.UrlPath)))
                .ForMember(pr => pr.Collaborators, opt => opt.MapFrom(pr => pr.Collaborators.Select(c => c.UserName).OrderBy(c => c)))
                .ForMember(pr => pr.IsLiked, opt => opt.MapFrom(pr => pr.Likes.Any(l => l.User.UserName == username)))
                .ForMember(pr => pr.IsFlagged, opt => opt.MapFrom(pr => pr.Flags.Any(fl => fl.User.UserName == username)));
        }
    }
}