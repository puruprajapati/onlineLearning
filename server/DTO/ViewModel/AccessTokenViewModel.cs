using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
  public class AccessTokenViewModel : BaseViewModel
  {
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public long Expiration { get; set; }



  }
}
