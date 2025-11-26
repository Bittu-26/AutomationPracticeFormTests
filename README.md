# 🤖 Resilient C# Selenium Automation Practice

This project contains automated End-to-End (E2E) tests written in **C#** using the **Selenium WebDriver** and **NUnit** framework.

The primary goal of this project is to demonstrate **resilient locator strategies** that make the automated tests stable and maintainable, even when the underlying HTML attributes (like IDs or class names) or the element positions change on the target web page.

## 🎯 Project Objective

The tests target the public practice form located at `http://app.cloudqa.io/home/AutomationPracticeForm`.

The test suite ensures correct functionality for core form interactions:

1. Entering data into the **First Name** field.

2. Selecting the **Gender (Female)** radio button.

3. Entering data into the **Date of Birth** field.

4. Clicking the **Submit** button.

## ✨ Resilient Automation Strategy

To meet the requirement of stability against UI changes, these tests deliberately avoid using volatile locators (e.g., dynamic IDs). Instead, they rely on visible, user-facing text and fundamental element properties.

| **Field Tested** | **Resilience Strategy** | **XPath Used** | 
| :--- | :--- | :--- | 
| **First Name** | Relative Traversal using Visible Label | `//label[contains(text(), 'First Name')]/following::input[1]` | 
| **Gender (Female)** | Direct Attribute Match (Type & Value) | `//input[@type='radio' and @value='Female']` | 
| **Date of Birth** | Relative Traversal using Visible Label | `//label[contains(text(), 'Date of Birth')]/following::input[1]` | 

## ⚙️ Setup and Dependencies

To run this project successfully, you will need the following environment configured:

1. **.NET SDK:** (.NET 8.0 or later)

2. **IDE:** Visual Studio or VS Code with the C# extensions.

3. **NuGet Packages:**

   * `NUnit`

   * `Selenium.WebDriver`

   * `Selenium.Support`

4. **ChromeDriver:** The `chromedriver.exe` that matches your Google Chrome version.

### 🚀 Execution Instructions

1. **Place ChromeDriver:** Ensure your `chromedriver.exe` is placed in an accessible location (e.g., `C:\SeleniumDrivers` or a dedicated folder).

2. **Update Path:** Review the `AutomationPracticeFormTests.cs` file and update the `baseDriverPath` variable in the `OneTimeSetup()` method to reflect the correct location of your `chromedriver.exe`.

   ```csharp
   // Update this path to the folder containing your chromedriver.exe
   string baseDriverPath = @"Your Location Here";
   
