﻿namespace Showcase.Server.DataTransferModels.Statistics
{
    using System;

    using AutoMapper;
    using MissingFeatures;
    using Newtonsoft.Json;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class TopProjectResponseModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MainImageUrl { get; set; }
        
        public int Likes { get; set; }

        public int Visits { get; set; }

        public int Comments { get; set; }

        public string TitleUrl
        {
            get
            {
                return this.Name.ToUrl();
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, TopProjectResponseModel>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(pr => pr.Title))
                .ForMember(pr => pr.Likes, opt => opt.MapFrom(pr => pr.Likes.Count))
                .ForMember(pr => pr.Visits, opt => opt.MapFrom(pr => pr.Visits.Count))
                .ForMember(pr => pr.Comments, opt => opt.MapFrom(pr => pr.Comments.Count))
                .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.Url + "." + pr.MainImage.FileExtension));
        }
    }
}