using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace OnlineLearning.Service.Interface
{
	public interface ISchoolService
	{
		Task<PagedList<School>> ListAsync(BaseParameter baseParameter);

		Task<SchoolResponse> UpdateAsync(Guid id, School school);
		Task<SchoolResponse> DeleteAsync(Guid id);
	}
}