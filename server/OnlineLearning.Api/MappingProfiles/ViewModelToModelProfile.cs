using AutoMapper;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Api.MappingProfiles
{
    public class ViewModelToModelProfile : Profile
    {
        public ViewModelToModelProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<StudentViewModel, Student>();
            CreateMap<TeacherViewModel, Teacher>();
            CreateMap<SchoolViewModel, School>();
            CreateMap<SessionViewModel, SessionDetail>();
            CreateMap<SectionViewModel, SectionDetail>();
            CreateMap<ClassViewModel, ClassDetail>();
            CreateMap<ParentViewModel, Parent>();
        }
    }
}