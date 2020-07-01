using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
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
  public class SessionService : ISessionService
  {
    private readonly ISessionRepository _sessionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork)
    {
      _sessionRepository = sessionRepository;
      _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<SessionDetail>> ListAsync(BaseParameter baseParameter)
    {
      return await _sessionRepository.GetCustomPaginatedList(baseParameter);
    }
    public async Task<SessionDetailResponse> CreateSessionAsync(SessionDetail sessionDetail, UserContextInfo userContext)
    {
      var result = await _sessionRepository.GetById(sessionDetail.Id);
      if (result != null)
      {
        return new SessionDetailResponse(false, "Session already in use.", null);
      }
      try
      {
        sessionDetail.CreatedByUserId = userContext.Id;
        sessionDetail.Active = ActiveStatus.Active.ToString();
        sessionDetail.SchoolId = userContext.SchoolId.Value;
        await _sessionRepository.Insert(sessionDetail);
        await _unitOfWork.CompleteAsync();

        return new SessionDetailResponse(true, null, sessionDetail);

      }
      catch (Exception ex)
      {
        return new SessionDetailResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
      }

    }

    public async Task<SessionDetailResponse> DeleteAsync(Guid id, UserContextInfo userContext)
    {
      var existingSessionDetail = await _sessionRepository.GetById(id);

      if (existingSessionDetail == null)
        return new SessionDetailResponse("SessionDetail not found.");

      try
      {
        _sessionRepository.Delete(id);
        await _unitOfWork.CompleteAsync();

        return new SessionDetailResponse(existingSessionDetail);
      }
      catch (Exception ex)
      {
        return new SessionDetailResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
      }
    }
    public async Task<SessionDetailResponse> FindByIdAsync(Guid id)
    {
      var sessionDetail = await _sessionRepository.GetById(id);
      if (sessionDetail == null)
        return new SessionDetailResponse("SessionDetail not found.");

      return new SessionDetailResponse(sessionDetail);
    }

    public async Task<SessionDetailResponse> UpdateAsync(Guid id, SessionDetail sessionDetail, UserContextInfo userContext)
    {
      var existingSessionDetail = await _sessionRepository.GetById(id);

      if (existingSessionDetail == null)
        return new SessionDetailResponse("SessionDetail not found.");

      existingSessionDetail.Active = sessionDetail.Active;
      existingSessionDetail.SessionDesc = sessionDetail.SessionDesc;
      existingSessionDetail.Active = sessionDetail.Active;
      existingSessionDetail.EndingTime = sessionDetail.EndingTime;
      existingSessionDetail.ModifiedAt = System.DateTime.Now;
      existingSessionDetail.ModifiedByUserId = userContext.Id;
      existingSessionDetail.ScheduledDate = sessionDetail.ScheduledDate;
      existingSessionDetail.SessionTitle = sessionDetail.SessionTitle;
      existingSessionDetail.StartingTime = sessionDetail.StartingTime;
      existingSessionDetail.TeacherId = sessionDetail.TeacherId;
      try
      {
        _sessionRepository.Update(existingSessionDetail);
        await _unitOfWork.CompleteAsync();

        return new SessionDetailResponse(existingSessionDetail);
      }
      catch (Exception ex)
      {
        return new SessionDetailResponse($"An error occurred when updating the session details: {ex.Message}");
      }
    }

    public async Task<SessionDetailResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
    {
      try
      {
        _sessionRepository.MultipleDelete(ids);
        await _unitOfWork.CompleteAsync();
        return new SessionDetailResponse($"Deleted successfully");
      }
      catch (Exception ex)
      {
        return new SessionDetailResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
      }
    }
  }
}