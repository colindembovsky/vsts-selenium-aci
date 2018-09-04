using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace WebApp.IntegrationTests
{
	[TestClass]
	public class IntegrationTests
	{
		static IWebDriver driver;
		static string HubUrl;
		static string BaseUrl;
		static string Browser;

		[AssemblyInitialize]
		public static void Setup(TestContext context)
		{
			HubUrl = context.Properties["HubUrl"].ToString();
			BaseUrl = context.Properties["BaseUrl"].ToString();
			Browser = context.Properties["Browser"].ToString();
			switch (Browser.ToLower())
			{
				case "chrome":
				{
					driver = new RemoteWebDriver(new Uri(HubUrl), new ChromeOptions());
					break;
				}
				case "firefox":
				{
					driver = new RemoteWebDriver(new Uri(HubUrl), new FirefoxOptions());
					break;
				}
				case "local":
				default:
				{
					driver = new ChromeDriver();
					break;
				}
			}
		}

		[AssemblyCleanup]
		public static void Cleanup()
		{
			driver.Quit();
		}

		[TestMethod]
		public void TestAboutPage()
		{
			driver.Navigate().GoToUrl($"{BaseUrl}");
		}
	}
}
