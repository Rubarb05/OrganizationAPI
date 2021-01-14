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
			APICalls.OrganizationAPI organizationAPI = new APICalls.OrganizationAPI(_logger, _config);

			//Make the 3 api calls.
			List<Model.Organization> organizations = await organizationAPI.GetOrganizationsAsync();

			foreach (var organization in organizations)
			{
				OrganizationPhone summary = new OrganizationPhone();
				summary.Id = organization.Id;
				summary.Name = organization.Name;

				//Get all users for the organization.
				List<Model.User> users = await organizationAPI.GetUsersAsync(organization.Id);
				
				foreach (var user in users)
				{
					List<Model.UserPhone> userPhones = await organizationAPI.GetUserPhonesAsync(organization.Id, user.Id);
					summary.AddUserSummary(new UserSummary { Id = user.Id, Email = user.Email, PhoneCount = userPhones.Count() });
					summary.TotalCount += userPhones.Count();
					summary.BlacklistTotal += userPhones.Where(p => p.Blacklist).Count();
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
