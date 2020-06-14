using AutoMapper;
using OnlineLearning.DTO.Response;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Api.MappingProfiles
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Student, StudentViewModel>();
            CreateMap<Teacher, TeacherViewModel>();
            CreateMap<School, SchoolViewModel>();
            CreateMap<SessionDetail, SessionViewModel>();
            CreateMap<SectionDetail, SectionViewModel>();
            CreateMap<ClassDetail, ClassViewModel>();
            CreateMap<Parent, ParentViewModel>();
            CreateMap<Attendence, AttendanceViewModel>();
            CreateMap<Assignment, AssignmentViewModel>();
            CreateMap<AssignmentSubmission, AssignmentSubmissionViewModel>();
            CreateMap<Grade, GradeViewModel>();
            CreateMap<MessageMain, MessageMainViewModel>();
            CreateMap<MessageReply, MessageReplyViewModel>();

            CreateMap<ReferenceType, ReferenceTypeViewModel>();
            CreateMap<SessionReference, SessionReferenceViewModel>();
            CreateMap<SessionStatus, SessionStatusViewModel>();
            CreateMap<Subject, SubjectViewModel>();
            CreateMap<SubmissionStatus, SubmissionStatusViewModel>();
            CreateMap<SubmitAssignment, SubmitAssignmentViewModel>();
            CreateMap<SubmitAssignmentAttachments, SubmitAssignmentAttachmentsViewModel>();
            CreateMap<TeacherSubject, TeacherSubjectViewModel>();
  
            CreateMap<AccessToken, AccessTokenViewModel>()
            .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
            .ForMember(a => a.RefreshToken, opt => opt.MapFrom(a => a.RefreshToken.Token))
            .ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration));
        }
    }
}