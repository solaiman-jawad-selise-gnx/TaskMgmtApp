using Application.Features.TaskMgmt.Commands;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DTOs;

namespace Presentation.MapperConfig;


public class TaskObjMapper
{
    public static Mapper InitializeAutomapper()
    {
        var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskItem, GetTaskDto>()
                    .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))

                    .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedToUser.Id))
                    .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedToUser.FullName))
                    .ForMember(dest => dest.AssignedUserRole,
                        opt => opt.MapFrom(src => nameof(src.AssignedToUser.Role)))
                    .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUser.Id))
                    .ForMember(dest => dest.CreatedUserName, opt => opt.MapFrom(src => src.CreatedByUser.FullName))
                    .ForMember(dest => dest.CreatedUserRole, opt => opt.MapFrom(src => nameof(src.CreatedByUser.Role)))
                    .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.Team.Id))
                    .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name));

                cfg.CreateMap<CreateTaskDto, CreateTaskCommand>();
                cfg.CreateMap<UpdateTaskDto, UpdateTaskCommand>();
            }
        );
        var mapper = new Mapper(config);
        return mapper;
    }
}