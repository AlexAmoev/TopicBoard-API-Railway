using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;
using Topic.Models;
using Topic.Models.Identity;

namespace Topic.Service
{
    public static class MappingInitializer
    {
        public static IMapper Initialize()
        {
            MapperConfiguration configuration = new(config =>
            {
                config.CreateMap<TopicEntity, TopicForAddingDTO>().ReverseMap();
                config.CreateMap<TopicEntity, TopicForGetingDTO>().ReverseMap();
                config.CreateMap<TopicEntity, TopicWithCommentsGetingDTO>().ReverseMap();
                config.CreateMap<TopicEntity, TopicWithCommentsGetingDTO>().ReverseMap();
                config.CreateMap<TopicEntity, TopicForUpdatingDTO>().ReverseMap(); 

                config.CreateMap<Comments, CommentForAddingDTO>().ReverseMap(); 
                config.CreateMap<Comments, CommentForGetingDTO>().ReverseMap();
                config.CreateMap<Comments, CommentWithTopicDTO>().ReverseMap();
                config.CreateMap<Comments, TopicWithCommentsGetingDTO>().ReverseMap();
                config.CreateMap<Comments, CommentForUpdatingDTO>().ReverseMap();

                config.CreateMap<UserBlock_UnblockDTO, User>().ReverseMap();
                config.CreateMap<UserInfo, User>()
                //.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.NormalizedUserName, options => options.MapFrom(source => source.Email.ToUpper()))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.NormalizedEmail, options => options.MapFrom(source => source.Email.ToUpper()))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNumber))
                .ReverseMap();
                config.CreateMap<UserUpdate, User>().ReverseMap();
                config.CreateMap<UserDTO, User>().ReverseMap()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNumber))
                .ReverseMap();

                config.CreateMap<RegistrationRequestDTO, User>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.NormalizedUserName, options => options.MapFrom(source => source.Email.ToUpper()))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.NormalizedEmail, options => options.MapFrom(source => source.Email.ToUpper()))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNumber));
            });

            return configuration.CreateMapper();
        }
    }
}
