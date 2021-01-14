using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OrganizationAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganizationAPITests
{
	public class OrganizationPhoneControllerTests
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
			OrganizationAPI.Controllers.OrganizationPhoneController controller = new OrganizationAPI.Controllers.OrganizationPhoneController(_logger, _configuration);
			Task<string> organizationSummary = controller.GetAsync();
			organizationSummary.Wait();

			Assert.IsTrue(!string.IsNullOrEmpty(organizationSummary.Result));
		}
	}
}