﻿namespace Showcase.Server.DataTransferModels.Project
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MissingFeatures;
    using Newtonsoft.Json;

    using Showcase.Data.Models;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Mapping;

    public class ProjectSimpleResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MainImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ShortDate
        {
            get
            {
                return this.CreatedOn.ToString(Constants.ShortDateFormat);
            }
        }

        public int Likes { get; set; }

        public int Visits { get; set; }

        public int Comments { get; set; }

        public int Flags { get; set; }

        public string TitleUrl
        {
            get
            {
                return this.Name.ToUrl();
            }
        }

        public IEnumerable<string> Collaborators { get; set; }

        public IEnumerable<TagResponseModel> Tags { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectSimpleResponseModel>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(pr => pr.Title))
                .ForMember(pr => pr.Likes, opt => opt.MapFrom(pr => pr.Likes.Count))
                .ForMember(pr => pr.Visits, opt => opt.MapFrom(pr => pr.Visits.Count))
                .ForMember(pr => pr.Comments, opt => opt.MapFrom(pr => pr.Comments.Count))
                .ForMember(pr => pr.Flags, opt => opt.MapFrom(pr => pr.Flags.Count))
                .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.UrlPath))
                .ForMember(pr => pr.Collaborators, opt => opt.MapFrom(pr => pr.Collaborators.Select(c => c.UserName).OrderBy(c => c)));
        }
    }
}
