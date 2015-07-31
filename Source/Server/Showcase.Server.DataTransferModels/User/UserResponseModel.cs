﻿namespace Showcase.Server.DataTransferModels.User
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    
    using Showcase.Data.Models;

    using Showcase.Server.Common.Mapping;
    using Showcase.Server.DataTransferModels.Comment;
    using Showcase.Server.DataTransferModels.Project;

    public class UserResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        public int ProjectsCount { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public IEnumerable<TeammateResponseModel> Teammates { get; set; }

        public IEnumerable<LikeResponseModel> Likes { get; set; }

        public IEnumerable<ProjectResponseModel> Projects { get; set; }

        public IEnumerable<CommentResponseModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserResponseModel>()
                .ForMember(u => u.ProjectsCount, opt => opt.MapFrom(u => u.Projects.Count))
                .ForMember(u => u.CommentsCount, opt => opt.MapFrom(u => u.Comments.Count))
                .ForMember(u => u.LikesCount, opt => opt.MapFrom(u => u.Likes.Count))
                .ForMember(
                    u => u.Teammates, 
                        opt => opt.MapFrom(u => 
                            u.Projects.SelectMany(
                                    p => p.Collaborators).Distinct()));
        }
    }
}