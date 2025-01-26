using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.Domain.Entities;

namespace TaskFlow.CrossCutting.IoC.Mapping
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<WorkItem, WorkItemDTO>();

            CreateMap<CreateWorkItemCommand, WorkItem>()
                .ConstructUsing(cmd => new WorkItem(cmd.Title, cmd.Description, DateTime.UtcNow));

            CreateMap<UpdateWorkItemCommand, WorkItem>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
