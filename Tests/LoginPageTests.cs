using ExpenseTracker.Automation.Pages;
using NUnit.Framework;

namespace ExpenseTracker.Automation.Tests;

public class LoginPageTests : BaseTest
{
    [Test]
    public async Task LoginPage_Loads_And_ShowsExpectedElements()
    {
        // Arrange
        var loginPage = new LoginPage(Page);

        // Act
        await loginPage.OpenAsync();
        await loginPage.WaitUntilLoadedAsync();

        // Assert
        Assert.That(
            await loginPage.WelcomeTitle.TextContentAsync(),
            Does.Contain("Welcome"));

        Assert.That(
            await loginPage.LoginButton.TextContentAsync(),
            Does.Contain("Login"));

        Assert.That(
            await loginPage.VersionLabel.IsVisibleAsync(),
            Is.True);
    }


    [Test]
    public async Task LoginPage_DisplaysAuthPromptText()
    {
        // Arrange
        var loginPage = new LoginPage(Page);

        // Act
        await loginPage.OpenAsync();
        await loginPage.WaitUntilLoadedAsync();

        var promptText = await loginPage.LoginMessage.TextContentAsync();

        // Assert
        Assert.That(
            promptText,
            Does.Contain("Please login with your Google account to continue"));
    }


    [Test]
    public async Task LoginPage_OpensGoogleAuthenticationPopup()
    {
        // Arrange
        var loginPage = new LoginPage(Page);

        await loginPage.OpenAsync();
        await loginPage.WaitUntilLoadedAsync();

        // Act
        var popup = await loginPage.OpenGoogleAuthenticationPopupAsync();

        // Assert
        Assert.That(popup, Is.Not.Null);

        Assert.That(
            popup.Url,
            Does.Contain("accounts.google.com"));

        Assert.That(
            await popup.TitleAsync(),
            Is.Not.Empty);
    }

    [Test]
    [Category("E2E")]
    public async Task LoginPage_SuccessfulGoogleLogin()
    {
        // Arrange
        var email = Environment.GetEnvironmentVariable("GOOGLE_EMAIL");
        var password = Environment.GetEnvironmentVariable("GOOGLE_PASSWORD");

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            Assert.Ignore("Google credentials are not configured.");
        }

        var loginPage = new LoginPage(Page);

        await loginPage.OpenAsync();
        await loginPage.WaitUntilLoadedAsync();

        // Act
        await loginPage.LoginWithGoogleAsync(email!, password!);

        // Assert
        Assert.That(
            Page.Url,
            Does.Contain("/home")
                .Or.Contain("/stats")
                .Or.Contain("/history")
                .Or.Contain("/insights"));
    }
}