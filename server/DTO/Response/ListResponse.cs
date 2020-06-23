using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
  public class ListRespose : BaseResponse
  {
    public ListViewModel Data { get; private set; }

    public ListRespose(bool success, string message, ListViewModel lvm) : base(success, message)
    {
      Data = lvm;
    }

    // success response
    public ListRespose(ListViewModel lvm) : this(true, string.Empty, lvm)
    { }

    // error response
    public ListRespose(string message) : this(false, message, null)
    { }
  }
}
