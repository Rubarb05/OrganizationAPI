using System;

namespace OrganizationAPI.Model
{
	public class UserPhone
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public int IMEI { get; set; }
		public bool Blacklist { get; set; }
	}
}
