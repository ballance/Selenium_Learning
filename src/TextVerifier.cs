using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace Selenium_learning
{
    public class VerifierStatus
    {
        public bool IsUp { get; set; }
        public string StatusText { get; set; }
    }

    public interface ITextVerifier
    {

        VerifierStatus Verify(IWebDriver driver, string url, string siteToTest, string testText);
    }

    public class TextVerifier : ITextVerifier
    {
        public VerifierStatus Verify(IWebDriver driver, string url, string siteToTest, string testText)
        {
            try
            {
                var verifierStatus = new VerifierStatus();
                driver.Navigate().GoToUrl(url);

                IWebElement domain_input = driver.FindElement(By.Id("domain_input"));
                domain_input.SendKeys(siteToTest);

                domain_input.Submit();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.PageSource.Contains("just you"));

                // Should see: "Cheese - Google Search" (for an English locale)
                var resultText = "result: " + driver.FindElement(By.Id("container")).Text;
                verifierStatus.StatusText = resultText;
                verifierStatus.IsUp = resultText.IndexOf(testText, StringComparison.InvariantCultureIgnoreCase) >= 0;

                return verifierStatus;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Verificion failed: " + ex);
            }
        }
    }

}
