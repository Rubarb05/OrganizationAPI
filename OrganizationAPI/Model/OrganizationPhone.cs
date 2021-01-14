using System.Collections.Generic;

namespace OrganizationAPI.Models
{
	public class OrganizationPhone
	{
		private List<UserSummary> _userSummaries;

		public OrganizationPhone()
		{
			_userSummaries = new List<UserSummary>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public int BlacklistTotal { get; set; }

		public int TotalCount { get; set; }

		public List<UserSummary> Users
		{
			get { return _userSummaries; }
			set
			{
				if (value != null)
				{
					_userSummaries = value;
				}
			}
		}

		public void AddUserSummary (UserSummary userSummary)
		{
			_userSummaries.Add(userSummary);
		}
	}
}
