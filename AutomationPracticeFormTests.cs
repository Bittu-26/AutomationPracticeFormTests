using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

[TestFixture]
public class AutomationPracticeFormTests
{
    private IWebDriver? driver; 
    private const string BaseUrl = "http://app.cloudqa.io/home/AutomationPracticeForm";
    private const int TimeoutSeconds = 30; 

    // This runs once before any tests start, initializing the browser and driver.
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        // Define the expected directory where the chromedriver.exe is located.
        string baseDriverPath = @"E:\Codes\Portfolio\AutomationPractice"; 
        string executableName = "chromedriver.exe";
        
        string driverDirectory;
        string expectedPath = Path.Combine(baseDriverPath, executableName);

        // Check if the driver is directly in the base path.
        if (File.Exists(expectedPath))
        {
            driverDirectory = baseDriverPath;
        }
        else
        {
            // If not found, check the common nested folder structure.
            string nestedFolderName = "chromedriver-win64"; 
            string nestedPath = Path.Combine(baseDriverPath, nestedFolderName);

            if (File.Exists(Path.Combine(nestedPath, executableName)))
            {
                driverDirectory = nestedPath;
            }
            else
            {
                // Throw a clear error if the driver is missing, guiding the user to fix the path.
                throw new FileNotFoundException(
                    $"The file '{executableName}' was not found in the specified path: '{baseDriverPath}' " +
                    $"or the common nested path: '{nestedPath}'. " +
                    "Please verify the exact location of your chromedriver.exe and update the 'baseDriverPath' variable."
                );
            }
        }

        // Start the ChromeDriver service using the verified location.
        var service = ChromeDriverService.CreateDefaultService(driverDirectory, executableName);
        driver = new ChromeDriver(service); 
        
        driver!.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(BaseUrl);
        
        // Use an implicit wait to help find dynamically loaded elements throughout the test.
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Explicitly wait for the main form content to appear ("First Name" text) before interacting.
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeoutSeconds));
        wait.Until(d => d!.FindElement(By.XPath("//*[contains(text(), 'First Name')]"))); 
        
        // Scroll the viewport to the top, ensuring elements are ready to be clicked.
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("window.scrollTo(0, 0);");
    }

    // This runs once after all tests are finished, closing the browser safely.
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    /// Test 1: Fills the First Name field.
    /// Resilience: Locates the input field by referencing its visible label text.
    [Test]
    public void Test_01_EnterFirstName_Successfully()
    {
        Console.WriteLine("Starting Test 1: Enter First Name");

        // Resilient XPath: Finds the input field relative to the "First Name" label.
        IWebElement firstNameInput = driver!.FindElement(By.XPath("//label[contains(text(), 'First Name')]/following::input[1]"));

        const string testName = "SuperMan";
        firstNameInput.SendKeys(testName);

        // Verification
        Assert.That(firstNameInput.GetAttribute("value"), Is.EqualTo(testName), "The entered value in the First Name field does not match the expected value.");
        Console.WriteLine($"Successfully entered '{testName}' into First Name field.");
    }

    /// Test 2: Selects the Male radio button for Gender.
    /// Resilience: Targets the input element directly using its stable type and value attributes.

    [Test]
    public void Test_02_SelectGenderFemale_Successfully()
    {
        Console.WriteLine("Starting Test 2: Select Gender (Male)");

        // Resilient XPath: Targets the radio button input based on its type and value attribute.
        IWebElement femaleRadioInput = driver!.FindElement(By.XPath("//input[@type='radio' and @value='Male']"));
        femaleRadioInput.Click();
        
        // Verification
        Assert.That(femaleRadioInput.Selected, Is.True, "The 'Male' radio button was not selected.");
        Console.WriteLine("Successfully selected 'Male' radio button.");
    }

    /// Test 3: Fills the Date of Birth field.
    /// Resilience: Locates the input field by referencing its visible label text.
    
    [Test]
    public void Test_03_EnterDateOfBirth_Successfully()
    {
        Console.WriteLine("Starting Test 3: Enter Date of Birth");

        // Resilient XPath: Finds the input field relative to the "Date of Birth" label.
        IWebElement dateOfBirthInput = driver!.FindElement(By.XPath("//label[contains(text(), 'Date of Birth')]/following::input[1]"));

        const string testDate = "01/05/2000"; 
        
        dateOfBirthInput.SendKeys(testDate);
        dateOfBirthInput.SendKeys(Keys.Enter);

        // Verification (Check that the year value was successfully registered)
        string actualValue = dateOfBirthInput.GetAttribute("value");
        Assert.That(actualValue, Does.Contain("2000"), "The entered date (2000) was not correctly registered in the Date of Birth field.");
        Console.WriteLine($"Successfully entered '{testDate}' into Date of Birth field.");
    }

    
    /// Test 4: Clicks the Submit button.
    /// Resilience: Targets the button directly by its visible text.
    
    [Test]
    public void Test_04_SubmitForm_Successfully()
    {
        Console.WriteLine("Starting Test 4: Submit Form");

        // Resilient XPath: Finds the button element based on its visible text.
        IWebElement submitButton = driver!.FindElement(By.XPath("//button[text()='Submit']"));
        submitButton.Click();
        
        Console.WriteLine("Successfully clicked the 'Submit' button.");
    }
}