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
		Logger<OrganizationAPI.Controllers.OrganizationPhoneController> _logger;
		IConfiguration _configuration;

		[SetUp]
		public void Setup()
		{
			_logger = new Logger<OrganizationAPI.Controllers.OrganizationPhoneController>(new LoggerFactory());
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
		public void GetUsersOrganization1Test()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<User>> users = externalAPI.GetUsersAsync(1);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
		}

		[Test]
		public void GetUsersOrganization2Test()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<User>> users = externalAPI.GetUsersAsync(2);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
		}

		[Test]
		public void GetUsersOrganization3Test()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<User>> users = externalAPI.GetUsersAsync(3);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
		}
		[Test]
		public void GetUsersOrganization4Test()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<User>> users = externalAPI.GetUsersAsync(4);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
		}

		[Test]
		public void GetUsersOrganization5Test()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<User>> users = externalAPI.GetUsersAsync(5);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
		}

		[Test]
		public void GetUsersOrganization6Test()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<User>> users = externalAPI.GetUsersAsync(5);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
		}

		[Test]
		public void GetPhonesTestOrg1User1()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);
			Task<List<UserPhone>> userPhones = externalAPI.GetUserPhonesAsync(1, 1);
			userPhones.Wait();

			Assert.IsTrue(userPhones.Result.Count > 0);
		}

		[Test]
		public void GetPhonesTestOrg2UserFirst()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);

			int orgId = 2;
			Task<List<User>> users = externalAPI.GetUsersAsync(orgId);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
			int userId = users.Result[0].Id;

			Task<List<UserPhone>> userPhones = externalAPI.GetUserPhonesAsync(orgId, userId);
			userPhones.Wait();

			Assert.IsTrue(userPhones.Result.Count > 0);
		}

		[Test]
		public void GetPhonesTestOrg3UserFirst()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);

			int orgId = 3;
			Task<List<User>> users = externalAPI.GetUsersAsync(orgId);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
			int userId = users.Result[0].Id;

			Task<List<UserPhone>> userPhones = externalAPI.GetUserPhonesAsync(orgId, userId);
			userPhones.Wait();

			Assert.IsTrue(userPhones.Result.Count > 0);
		}

		[Test]
		public void GetPhonesTestOrg4UserFirst()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);

			int orgId = 4;
			Task<List<User>> users = externalAPI.GetUsersAsync(orgId);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
			int userId = users.Result[0].Id;

			Task<List<UserPhone>> userPhones = externalAPI.GetUserPhonesAsync(orgId, userId);
			userPhones.Wait();

			Assert.IsTrue(userPhones.Result.Count > 0);
		}

		[Test]
		public void GetPhonesTestOrg5UserFirst()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);

			int orgId = 5;
			Task<List<User>> users = externalAPI.GetUsersAsync(orgId);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
			int userId = users.Result[0].Id;

			Task<List<UserPhone>> userPhones = externalAPI.GetUserPhonesAsync(orgId, userId);
			userPhones.Wait();

			Assert.IsTrue(userPhones.Result.Count > 0);
		}

		[Test]
		public void GetPhonesTestOrg6UserFirst()
		{
			OrganizationAPI.APICalls.OrganizationAPI externalAPI = new OrganizationAPI.APICalls.OrganizationAPI(_logger, _configuration);

			int orgId = 6;
			Task<List<User>> users = externalAPI.GetUsersAsync(orgId);
			users.Wait();

			Assert.IsTrue(users.Result.Count > 0);
			int userId = users.Result[0].Id;

			Task<List<UserPhone>> userPhones = externalAPI.GetUserPhonesAsync(orgId, userId);
			userPhones.Wait();

			Assert.IsTrue(userPhones.Result.Count > 0);
		}
	}
}