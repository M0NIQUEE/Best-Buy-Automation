// Built a simple automation log in for best buy.
// The automation will go ahead and input credentials all the way until
// you are asked to input one time code, which the user will have to manually input
// but once it is manually intput, welcome to best buy!

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Automation
{
    class Program
    {
        // Create a reference for Chrome browser
        IWebDriver driver = new ChromeDriver();

        [SetUp]
        public void Initialize()
        {
            // Go to Google Page
            driver.Navigate().GoToUrl("https://www.bestbuy.com");
        }

        [Test]
        public void ExecuteTest()
        {
            // Make the browser full screen
            driver.Manage().Window.Maximize();

            // Web Elements
            // Because you must wait for the sign in to load must add in a wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement account_block = wait.Until(d =>
                d.FindElement(By.Id("account-menu-account-button"))
            );
            account_block.Click();

            IWebElement sign_in = wait.Until(d =>
                d.FindElement(By.XPath("//a[text()='Sign In']")));
            sign_in.Click();

            IWebElement email_field = driver.FindElement(By.Id("fld-e"));
            // For testing it's best to use an actual best buy email 
            // ************* ENTER EMAIL HERE**********************
            email_field.SendKeys("example@email.com");

            IWebElement submit = driver.FindElement(By.XPath("//button[@type='submit']"));
            submit.Click();

            // For testing use an account that has MFA with a phone number and input
            // the last 4 digits that are connectd to the account
            IWebElement digits = wait.Until(e => e.FindElement(By.Id("smsDigits")));
            // *************ENTER LAST 4 HERE **************************
            digits.SendKeys("1111");

            // For testing enter the last name that is connected to the account for MFA
            IWebElement last_name = driver.FindElement(By.Id("lastName"));
            // ***********ENTER LAST NAME HERE **************************
            last_name.SendKeys("examplelastname");

            // Continue
            IWebElement continue_but = driver.FindElement(By.XPath("/html/body/div[1]/div/section/main/div[2]/div/div/div[1]/div/div/div/div/div/form/div[3]/button[1]"));
            continue_but.Click();

            // That should lead to getting the one time code and log into your account by
            // manually inputting since the one time code will always be different
            // but once passed WELCOME TO BEST BUY

        }

        // Method that would automatically close the browser
        // If wanted to close browser immediatley after testing then uncomment below
        [TearDown]
        public void CloseTest()
        {
            // driver.Quit();
        }
    }
}