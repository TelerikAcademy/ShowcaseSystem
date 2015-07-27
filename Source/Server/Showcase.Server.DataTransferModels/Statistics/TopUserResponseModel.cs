﻿namespace Showcase.Server.DataTransferModels.Statistics
{
    using System.Linq;

    using AutoMapper;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class TopUserResponseModel : IMapFrom<User>
    {
        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        public int ProjectsCount { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, TopUserResponseModel>()
                .ForMember(u => u.ProjectsCount, opt => opt.MapFrom(u => u.Projects.Count))
                .ForMember(u => u.LikesCount, opt => opt.MapFrom(u => u.Projects.Sum(pr => pr.Likes.Count)));
        }
    }
}