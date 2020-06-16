using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using OnlineLearning.Service.Interface;
using OnlineLearning.Shared.Enums;
using OnlineLearning.Shared.Interface.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service
{
  public class ListService : IListService
  {
    private ISchoolRepository _schoolRepository;
    private IClassRepository _classRepository;
    private ISectionRepository _sectionRepository;
    private IStudentRepository _studentRepository;
    private ITeacherRepository _teacherRepository;

    public ListService(
    ISchoolRepository schoolRepository,
    IClassRepository classRepository,
    ISectionRepository sectionRepository,
    IStudentRepository studentRepository,
    ITeacherRepository teacherRepository)
    {
      _schoolRepository = schoolRepository;
      _classRepository = classRepository;
      _sectionRepository = sectionRepository;
      _studentRepository = studentRepository;
      _teacherRepository = teacherRepository;

    }

    public async Task<List<List<ListViewModel>>> GetAllList(UserContextInfo userContext)
    {
      List<List<ListViewModel>> result = new List<List<ListViewModel>>();
      var schoolList = await _schoolRepository.getList();
      result.Add(schoolList);

      if (userContext.SchoolId != null && userContext.SchoolId != Guid.Empty)
      {
        Guid schoolId = new Guid(userContext.SchoolId.ToString());
        var classList = await _classRepository.getList(schoolId);
        result.Add(classList);

        var sectionList = await _sectionRepository.getList(schoolId);
        result.Add(sectionList);

        var studentList = await _studentRepository.getList(schoolId);
        result.Add(studentList);

        var teacherList = await _teacherRepository.getList(schoolId);
        result.Add(teacherList);
      }

      return result;
    }

  }
}