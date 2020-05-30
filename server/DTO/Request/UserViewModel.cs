using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Request
{
	public class UserViewModel
	{
    public Guid Id { get; set; }
    public Guid? SchoolId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string UserRole { get; set; }
    public bool IsVerified { get; set; }
    public bool IsValid { get; set; }
  }
}
