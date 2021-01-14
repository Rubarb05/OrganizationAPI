using System;

namespace OrganizationAPI.Model
{
	public class User
	{
		public int Id { get; set; }
		public int OrganizationId { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
