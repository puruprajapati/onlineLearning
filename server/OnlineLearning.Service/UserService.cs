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
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
      _passwordHasher = passwordHasher;
      _unitOfWork = unitOfWork;
      _userRepository = userRepository;
    }

    public async Task<PagedList<User>> ListAsync(BaseParameter baseParameter)
    {
      return await _userRepository.GetPaginatedList(baseParameter);
    }
    public async Task<UserResponse> CreateUserAsync(User user)
    {
      var existingUser = await _userRepository.FindByUserName(user.UserName);
      if (existingUser != null)
      {
        return new UserResponse(false, "UserName already in use.", null);
      }
      try
      {
        user.Password = _passwordHasher.HashPassword(user.Password);
        user.Active = ActiveStatus.Active.ToString();

        await _userRepository.Insert(user);
        await _unitOfWork.CompleteAsync();

        return new UserResponse(true, null, user);

      }
      catch (Exception ex)
      {
        return new UserResponse($"An error occurred when saving the user: {ex.Message}");
      }

    }

    public async Task<UserResponse> DeleteAsync(Guid id)
    {
      var existingUser = await _userRepository.GetById(id);

      if (existingUser == null)
        return new UserResponse("User not found.");

      try
      {
        _userRepository.Delete(id);
        await _unitOfWork.CompleteAsync();

        return new UserResponse(existingUser);
      }
      catch (Exception ex)
      {
        return new UserResponse($"An error occurred when deleting the user: {ex.Message}");
      }
    }

    public Task<UserResponse> FindByIdAsync(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<User> FindByEmailAsync(string email)
    {
      throw new NotImplementedException();
    }

    public async Task<User> FindByUsernameAsync(string userName)
    {
      return await _userRepository.FindByUserName(userName);
    }

    public Task<UserResponse> UpdateAsync(Guid id, User user, UserContextInfo userContext)
    {
      throw new NotImplementedException();
    }

    public async Task<UserResponse> DeleteAsync(Guid id, UserContextInfo userContext)
    {
      var existingUser = await _userRepository.GetById(id);

      if (existingUser == null)
        return new UserResponse("School not found.");

      try
      {
        _userRepository.Delete(id);
        await _unitOfWork.CompleteAsync();

        return new UserResponse(existingUser);
      }
      catch (Exception ex)
      {
        return new UserResponse($"An error occurred when deleting the school: {ex.Message}");
      }
    }

    public async Task<UserResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
    {
      _userRepository.MultipleDelete(ids);
      await _unitOfWork.CompleteAsync();
      return new UserResponse($"Deleted successfully");
    }
  }
}
