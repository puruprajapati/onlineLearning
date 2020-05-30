using AutoMapper;
using OnlineLearning.DTO.Response;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Api.MappingProfiles
{
	public class ModelToViewModelProfile: Profile
	{
		public ModelToViewModelProfile() 
		{
			CreateMap<User, UserViewModel>();
			CreateMap<AccessToken, AccessTokenViewModel>()
				.ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
				.ForMember(a => a.RefreshToken, opt => opt.MapFrom(a => a.RefreshToken.Token))
				.ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration));
		}
	}
}
