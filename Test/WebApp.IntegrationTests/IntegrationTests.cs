using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.IO;

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
					driver = new ChromeDriver(Directory.GetCurrentDirectory());
					break;
				}
			}

			Trace.WriteLine($"HubUrl: {HubUrl}");
			Trace.WriteLine($"BaseURL: {BaseUrl}");
			Trace.WriteLine($"Browser: {Browser}");
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
			// click the About link in the nav bar
			driver.FindElement(By.LinkText("About")).Click();

			// check that we're on the About page
			var header = driver.FindElement(By.XPath("//*/h2[contains(text(), 'About.')]"));

			// check that the H3 text is correct
			var searchWord = "description"; // this should pass
			//var searchWord = "webapp"; // this should fail
			var aboutBlurb = driver.FindElement(By.XPath($"//*/h3[contains(text(), '{searchWord}')]"));
		}
	}
}
