﻿using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Selenium;

namespace FluffySpoon.Automation.Web.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
			try
			{
				var serviceCollection = new ServiceCollection();
				serviceCollection.UseJQueryDomSelector();
				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetChromeDriver());

				var serviceProvider = serviceCollection.BuildServiceProvider();

				var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>();
				await automationEngine.InitializeAsync();

				await automationEngine
					.Open("https://google.com")
					.Enter("foobar").In("input[type=text]")
					.Wait(TimeSpan.FromSeconds(1))
					.Expect
					.Exists(".lsb");

				await automationEngine
					.Click.On("#sbtc .lsb:first")
					.Wait(TimeSpan.FromSeconds(1))
					.Expect
					.Count(10).Of(".g");

				Console.WriteLine("Test done!");
			}
			catch (Exception ex) {
				await Console.Error.WriteLineAsync(ex.ToString());
				Console.ReadLine();
			}
        }

		private static ChromeDriver GetChromeDriver()
		{
			var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
			service.EnableVerboseLogging = false;
			service.HideCommandPromptWindow = true;
			service.SuppressInitialDiagnosticInformation = true;

			var chromeDriver = new ChromeDriver(Environment.CurrentDirectory, new ChromeOptions()
			{
				Proxy = null,
				PageLoadStrategy = PageLoadStrategy.Normal,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept				
			});
			chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
			return chromeDriver;
		}
	}
}
