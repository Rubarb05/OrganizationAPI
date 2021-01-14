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
		private readonly string _usersEndpoint;
		private readonly string _phonesEndpoint;

		private static HttpClient _client;

		public OrganizationAPI(ILogger<OrganizationAPI> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
			_client = new HttpClient();

			//pull base api from appSettings.
			_organizationBaseAPI = _config.GetValue<string>("ConnectionStrings:OrganizationBaseAPI");
			_usersEndpoint = _config.GetValue<string>("ConnectionStrings:UsersEndPoint");
			_phonesEndpoint = _config.GetValue<string>("ConnectionStrings:UserPhonesEndPoint");
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

		public async Task<List<Model.User>> GetUsersAsync(int organizationId)
		{
			List<Model.User> users = new List<Model.User>();
			string organizationIdPath = string.Format("/{0}", organizationId);
			string apiEndpoint = _organizationBaseAPI + organizationIdPath + _usersEndpoint;

			_logger.LogInformation(apiEndpoint);

			//Ensure the user api call is setup correctly
			if (!string.IsNullOrEmpty(apiEndpoint))
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

		public async Task<List<Model.UserPhone>> GetUserPhonesAsync(int organizationId, int userId)
		{
			List<Model.UserPhone> userPhones = new List<Model.UserPhone>();
			string organizationIdPath = string.Format("/{0}", organizationId);
			string organizationPath = _organizationBaseAPI + organizationIdPath + _usersEndpoint;
			string userIdPath = string.Format("/{0}", userId);
			string apiEndpoint = organizationPath + userIdPath + _phonesEndpoint;

			_logger.LogInformation(apiEndpoint);

			//Ensure the api call is setup correctly
			if (!string.IsNullOrEmpty(apiEndpoint))
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
