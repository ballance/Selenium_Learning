using Autofac;
using Autofac.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;

namespace Selenium_learning
{
    public static class BootStrapper
    {
        public static IContainer Run()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TextVerifier>().As<ITextVerifier>();
            builder.RegisterModule<WebDriverModule>();

            return builder.Build();
        }
    }

    public class WebDriverModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChromeDriver>().As<IWebDriver>();
            builder.RegisterType<PhantomJSDriver>().As<IWebDriver>();
            
            // BUG: Seeing an issue when FireFox tries to load.  This previously worked but broke after adding PhantomJsDriver nugets
            //builder.RegisterType<FirefoxDriver>().As<IWebDriver>();
        }
    }
}