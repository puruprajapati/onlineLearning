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
            CreateMap<AssignmentViewModel, Assignment>();
            CreateMap<AssignmentSubmissionViewModel, AssignmentSubmission>();
            CreateMap<AttendanceViewModel, Attendence>();
            CreateMap<GradeViewModel, Grade>();
            CreateMap<MessageMainViewModel, MessageMain>();
            CreateMap<MessageReplyViewModel, MessageReply>();
            CreateMap<StudentViewModel, Student>();
            CreateMap<TeacherViewModel, Teacher>();
            CreateMap<SchoolViewModel, School>();
            CreateMap<SessionViewModel, SessionDetail>();
            CreateMap<SectionViewModel, SectionDetail>();
            CreateMap<ClassViewModel, ClassDetail>();
            CreateMap<ParentViewModel, Parent>();
            //CreateMap<AttendanceViewModel, Attendence>();
            CreateMap<ReferenceTypeViewModel, ReferenceType>();
            CreateMap<SessionReferenceViewModel, SessionReference>();
            CreateMap<SessionStatusViewModel, SessionStatus>();
            CreateMap<SubjectViewModel, Subject>();
            CreateMap<SubmissionStatusViewModel, SubmissionStatus>();
            CreateMap<SubmitAssignmentViewModel, SubmitAssignment>();
            CreateMap<SubmitAssignmentAttachmentsViewModel, SubmitAssignmentAttachments>();
            CreateMap<TeacherSubjectViewModel, TeacherSubject>();
        }
    }
}