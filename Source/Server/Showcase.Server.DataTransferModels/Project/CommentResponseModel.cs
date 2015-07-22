﻿namespace Showcase.Server.DataTransferModels.Project
{
    using System;

    using AutoMapper;
    using MissingFeatures;
    using Newtonsoft.Json;

    using Showcase.Data.Models;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Mapping;

    public class CommentResponseModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int ProjectId { get; set; }

        public string ProjectTitle { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public string ProjectTitleUrl
        {
            get
            {
                return this.ProjectTitle.ToUrl();
            }
        }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; }

        public string ShortDate
        {
            get
            {
                return this.CreatedOn.ToString(Constants.ShortDateFormat);
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentResponseModel>()
                .ForMember(c => c.Username, opt => opt.MapFrom(c => c.User.UserName));
        }
    }
}