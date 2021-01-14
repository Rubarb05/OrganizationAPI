using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationAPI.Models
{
	public class UserSummary
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public int PhoneCount { get; set; }
	}
}
