using System;
using Autofac;
using OpenQA.Selenium;

namespace Selenium_learning
{
    class Program
    {
        private static IContainer container;

        static void Main(string[] args)
        {
            container = BootStrapper.Run();
            using (var scope = container.BeginLifetimeScope())
            {
                var testUrl = "http://downforeveryoneorjustme.com";
                var siteToTest = "http://chrisballance.com";

                var textVerifier = scope.Resolve<ITextVerifier>();
                var driver = scope.Resolve<IWebDriver>();
                var retval = textVerifier.Verify(driver, testUrl, siteToTest, "is up");

                Console.WriteLine("The site [{0}] is {1}.", siteToTest, (retval.IsUp ? "up" : "down"));
                Console.ReadKey();
            }
        }
    }
}
