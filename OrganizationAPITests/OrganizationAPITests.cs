using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OrganizationAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganizationAPITests
{
	public class OrganizationAPITests
	{
		Logger<OrganizationAPI.APICalls.OrganizationAPI> _logger;
		IConfiguration _configuration;

		[SetUp]
		public void Setup()
		{
			_logger = new Logger<OrganizationAPI.APICalls.OrganizationAPI>(new LoggerFactory());
			_configuration = new ConfigurationBuilder().AddJsonFile("appSettings.Json").Build();
		}

		[Test]
		public void GetOrganizationsTest()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<Organization>> organizations = externalAPI.GetOrganizationsAsync();
			organizations.Wait();

			Assert.IsTrue(organizations.Result.Count > 0);
		}

		[Test]
		public void GetUsersTest()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<User>> users = externalAPI.GetUsersAsync(1);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
		}

		[Test]
		public void GetPhonesTest()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<UserPhone>> userPhones = externalAPI.GetUserPhonesAsync(1, 1);
			userPhones.Wait();

			Assert.IsTrue(userPhones.Result.Count > 0);
		}
	}
}