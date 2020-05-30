using AutoMapper;
using DTO.Request;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.MappingProfiles
{
	public class ViewModelToModelProfile : Profile
	{
		public ViewModelToModelProfile() 
		{
			CreateMap<User, UserViewModel>();
		}
		
	}
}
