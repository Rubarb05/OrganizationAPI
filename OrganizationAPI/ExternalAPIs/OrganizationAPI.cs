using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationAPI.APICalls
{
	public class OrganizationAPI
	{
		private readonly ILogger<OrganizationAPI> _logger;
		private readonly IConfiguration _config;
		private readonly string _organizationBaseAPI;

		private static HttpClient _client;

		public OrganizationAPI(ILogger<OrganizationAPI> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
			_client = new HttpClient();

			//pull base api from appSettings.
			_organizationBaseAPI = _config.GetValue<string>("ConnectionStrings:OrganizationBaseAPI");
		}

		public async Task<List<Model.Organization>> GetOrganizationsAsync()
		{
			List<Model.Organization> organizations = new List<Model.Organization>();
			_logger.LogInformation(_organizationBaseAPI);

			//Ensure the organization api call is setup correctly
			if (!string.IsNullOrEmpty(_organizationBaseAPI))
			{
				//API call for Organization Data
				HttpResponseMessage response = await _client.GetAsync(_organizationBaseAPI);

				if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync();
					//Convert Organization Data from String to modeled object
					organizations = JsonConvert.DeserializeObject<List<Model.Organization>>(responseString);
					_logger.LogInformation(responseString);
					_logger.LogInformation(organizations.Count().ToString());
				}
			}
			return organizations;
		}

		public async Task<List<Model.User>> GetUsersAsync()
		{
			List<Model.User> users = new List<Model.User>();
			string userEndpoint = _config.GetValue<string>("ConnectionStrings:UsersEndPoint");
			string apiEndpoint = _organizationBaseAPI + userEndpoint;

			_logger.LogInformation(apiEndpoint);

			//Ensure the user api call is setup correctly
			if (!string.IsNullOrEmpty(apiEndpoint) && !string.IsNullOrEmpty(userEndpoint))
			{
				//API call for User Data
				HttpResponseMessage response = await _client.GetAsync(apiEndpoint);

				if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync();
					//Convert User Data from String to modeled object
					users = JsonConvert.DeserializeObject<List<Model.User>>(responseString);
					_logger.LogInformation(responseString);
					_logger.LogInformation(users.Count().ToString());
				}
			}

			return users;
		}

		public async Task<List<Model.UserPhone>> GetUserPhonesAsync()
		{
			List<Model.UserPhone> userPhones = new List<Model.UserPhone>();
			string userPhoneEndpoint = _config.GetValue<string>("ConnectionStrings:UserPhonesEndPoint");
			string apiEndpoint = _organizationBaseAPI + userPhoneEndpoint;

			_logger.LogInformation(apiEndpoint);

			//Ensure the user api call is setup correctly
			if (!string.IsNullOrEmpty(apiEndpoint) && !string.IsNullOrEmpty(userPhoneEndpoint))
			{
				//API call for Phone Data
				HttpResponseMessage response = await _client.GetAsync(apiEndpoint);

				if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync();
					//Convert Phone Data from String to modeled object
					userPhones = JsonConvert.DeserializeObject<List<Model.UserPhone>>(responseString);
					_logger.LogInformation(responseString);
					_logger.LogInformation(userPhones.Count().ToString());
				}
			}

			return userPhones;
		}
	}
}
