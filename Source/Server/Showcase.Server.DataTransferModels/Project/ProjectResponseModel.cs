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
    using Showcase.Server.DataTransferModels.File;
    using Showcase.Server.DataTransferModels.Tag;
    using Showcase.Server.DataTransferModels.User;

    public class ProjectResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public const int ShortDescriptionLength = 300;

        public int Id { get; set; }

        public string Title { get; set; }

        public string MainImageUrl { get; set; }

        public string Description { get; set; }

        public string VideoEmbedSource { get; set; }

        public string RepositoryUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Visits { get; set; }

        public int Comments { get; set; }

        public IEnumerable<CollaboratorResponseModel> Collaborators { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }

        public IEnumerable<TagResponseModel> Tags { get; set; }

        public IEnumerable<FileInfoResponseModel> Files { get; set; }

        public bool IsLiked { get; set; }

        public bool IsFlagged { get; set; }

        public bool IsHidden { get; set; }

        public string TitleUrl
        {
            get
            {
                return this.Title.ToUrl();
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
                if (this.Description.Length <= ShortDescriptionLength)
                {
                    return this.Description;
                }
                else
                {
                    return this.Description.Substring(0, ShortDescriptionLength) + "...";
                }
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            string username = null;
            configuration.CreateMap<Project, ProjectResponseModel>()
                .ForMember(pr => pr.Likes, opt => opt.MapFrom(pr => pr.Likes.Count))
                .ForMember(pr => pr.Visits, opt => opt.MapFrom(pr => pr.Visits.Count))
                .ForMember(pr => pr.Comments, opt => opt.MapFrom(pr => pr.Comments.Count))
                .ForMember(pr => pr.Tags, opt => opt.MapFrom(pr => pr.Tags.OrderBy(t => t.Type)))
                .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.UrlPath))
                .ForMember(pr => pr.ImageUrls, opt => opt.MapFrom(pr => pr.Images.Select(i => i.UrlPath)))
                .ForMember(pr => pr.IsLiked, opt => opt.MapFrom(pr => pr.Likes.Any(l => l.User.UserName == username)))
                .ForMember(pr => pr.IsFlagged, opt => opt.MapFrom(pr => pr.Flags.Any(fl => fl.User.UserName == username)));
        }
    }
}