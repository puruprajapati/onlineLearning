﻿using AutoMapper;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.MappingProfiles
{
	public class ViewModelToModelProfile : Profile
	{
		public ViewModelToModelProfile() 
		{
			CreateMap<User, UserViewModel>();
		}
		
	}
}
