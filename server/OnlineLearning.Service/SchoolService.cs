using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using OnlineLearning.Service.Interface;
using OnlineLearning.Shared.Interface.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service
{
	public class SchoolService : ISchoolService
	{
		private readonly ISchoolRepository _schoolRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SchoolService(ISchoolRepository schoolRepository, IUnitOfWork unitOfWork)
		{
			_schoolRepository = schoolRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<PagedList<School>> ListAsync(BaseParameter baseParameter)
		{
			return await _schoolRepository.GetPaginatedList(baseParameter);
		}


		public async Task<SchoolResponse> DeleteAsync(Guid id)
		{
			var existingSchool = await _schoolRepository.GetById(id);

			if (existingSchool == null)
				return new SchoolResponse("School not found.");

			try
			{
				_schoolRepository.Delete(id);
				await _unitOfWork.CompleteAsync();

				return new SchoolResponse(existingSchool);
			}
			catch (Exception ex)
			{
				return new SchoolResponse($"An error occurred when deleting the school: {ex.Message}");
			}
		}


		public Task<SchoolResponse> FindByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<SchoolResponse> UpdateAsync(Guid id, School school)
		{
			throw new NotImplementedException();
		}
	}
}