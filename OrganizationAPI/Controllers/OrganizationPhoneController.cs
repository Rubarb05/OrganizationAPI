using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrganizationAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class OrganizationPhoneController : ControllerBase
	{
		private readonly ILogger<OrganizationPhoneController> _logger;
		private readonly IConfiguration _config;

		public OrganizationPhoneController(ILogger<OrganizationPhoneController> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}

		[HttpGet]
		public async Task<string> GetAsync()
		{
			List<OrganizationPhone> phoneSummary = new List<OrganizationPhone>();
			APICalls.OrganizationAPI organizationAPI = new APICalls.OrganizationAPI(new Logger<APICalls.OrganizationAPI>(new LoggerFactory()), _config);

			//Make the 3 api calls.
			List<Model.Organization> organizations = await organizationAPI.GetOrganizationsAsync();
			List<Model.User> users = await organizationAPI.GetUsersAsync();
			List<Model.UserPhone> userPhones = await organizationAPI.GetUserPhonesAsync();

			//Create a combined list of users with their phones, defaulting to null values if no phone is found with the user.
			var userPhoneBreakdown = (from u in users
									join up in userPhones
										on u.Id equals up.UserId into upSummary
									from phones in upSummary.DefaultIfEmpty()
									select new
									{
										UserId = u.Id,
										OrganizationId = u.OrganizationId,
										UserEmail = u.Email,
										UserPhoneId = phones?.Id ?? null,
										BlackListedPhone = phones?.Blacklist ?? null
									}).ToList();

			//Create a combined list of organizations with their users from the combined user-phone list, defaulting to null values if no user is found.
			var organizationPhoneBreakdown = (from o in organizations
									  join u in userPhoneBreakdown
										  on o.Id equals u.OrganizationId into userOrganizationSummary
									  from user in userOrganizationSummary.DefaultIfEmpty()
									  select new
									  {
										  OrganizationId = o.Id,
										  OrganizationName = o.Name,
										  UserId = user?.UserId ?? null,
										  UserEmail = user?.UserEmail ?? null,
										  UserPhoneId = user?.UserPhoneId ?? null,
										  BlackListed = user?.BlackListedPhone ?? null
									  }).ToList();

			//Group the combined list by OrganizationId to ensure only a single entry per organization is returned.
			var organizationGroupBy = (from organization in organizationPhoneBreakdown
									   group organization by organization.OrganizationId into newGroup
									   orderby newGroup.Key
									   select newGroup);

			foreach (var organizationPhone in organizationGroupBy)
			{
				OrganizationPhone summary = new OrganizationPhone();
				summary.Id = organizationPhone.Key;
				summary.Name = organizationPhone.FirstOrDefault().OrganizationName;
				summary.BlacklistTotal = organizationPhone.Where(p => p.BlackListed.HasValue && p.BlackListed.Value).Count();
				summary.TotalCount = organizationPhone.Where(p => p.UserPhoneId.HasValue).Count();
				
				//Pull all unique userIds associated with the current organization to determine their phone counts and reduce user list to a single listing.
				var userIds = organizationPhone.Where(u => u.UserId.HasValue).Select(u => u.UserId.Value).Distinct();
				foreach (var userId in userIds)
				{
					UserSummary userSummary = new UserSummary();
					userSummary.Id = organizationPhone.Where(u => u.UserId == userId).Select(u => u.UserId.Value).FirstOrDefault();
					userSummary.Email = organizationPhone.Where(u => u.UserId == userId).Select(u => u.UserEmail).FirstOrDefault();
					userSummary.PhoneCount = organizationPhone.Where(u => u.UserId == userId && u.UserPhoneId.HasValue).Count();
					summary.AddUserSummary(userSummary);
				}

				phoneSummary.Add(summary);
			}

			//Ensure the output is in json.
			string phoneSummaryString = JsonConvert.SerializeObject(phoneSummary);
			_logger.LogInformation(phoneSummaryString);

			return phoneSummaryString;
		}
	}
}
