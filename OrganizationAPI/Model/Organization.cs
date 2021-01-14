using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationAPI.Model
{
	public class Organization
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Name { get; set; }
	}
}
